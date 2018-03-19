using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    [SyncVar]
    private uint answeredQuestionsGlobalCount;

    private uint answeredQuestionsLocalCount;
    private bool timerActive;
    private bool allAnswered;
    private bool answerWindowFinished;

    [HideInInspector]
    [SyncVar]
    public bool answerDone;

    private Color32 colorProcess = new Color32(225, 216, 98, 255);
    private Color32 colorSuccess = new Color32(98, 225, 114, 255);
    private Color32 colorError = new Color32(225, 98, 98, 255);

    public PlayerData playerData;

    [SerializeField]
    private Transform playerFigureTransform;

    [SerializeField]
    private Transform[] waypointsTransforms;

    [Header("Question Window")]

    [SerializeField]
    private GameObject questionWindow;

    [SerializeField]
    private GameObject timerObject;

    [SerializeField]
    private GameObject leaderboard;

    [SerializeField]
    private GameObject buttonExit;

    [SerializeField]
    private Text resultText;

    [SerializeField]
    private GameObject[] questionsGO;

    [Header("Timer")]

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Image timerFillImage;

    private QuestionData currentQuestionData;

    private const string pointTag = "Point";
    private const string answerTag = "Answer";
    
    [Server]
    public void StartGame()
    {
        Debug.Log("GameManager started.");
        questionWindow.SetActive(false);
        buttonExit.SetActive(false);
        foreach (GameObject question in questionsGO)
        {
            question.SetActive(false);
        }

        RpcAllPlayersRefresh();
        RpcQuestionPopUp();
    }

    [ClientRpc]
    private void RpcAllPlayersRefresh()
    {
        playerData.Refresh();
    }

    [ClientRpc]
    private void RpcQuestionPopUp()
    {
        if (answeredQuestionsGlobalCount < questionsGO.Length)
        {
            // If there's still questions in list

            currentQuestionData = questionsGO[answeredQuestionsGlobalCount].GetComponent<QuestionData>();

            Debug.Log("Question: " + (currentQuestionData.number) +
                      ". MaxScore: " + currentQuestionData.scoreMaxValue +
                      ". Duration: " + currentQuestionData.answerDuration + " s");
            
            if (NetworkServer.active)
            {
                PointsControl();
            }
        }
        else
        {
            // If questions are ended

            RpcTheEnd();
            Debug.Log("Final!");
        }
    }

    [Server]
    private void PointsControl()
    {
        StartCoroutine(WindowShow(currentQuestionData.answerDuration));
    }

    private IEnumerator WindowShow(float duration)
    {
        RpcInitWindows(answeredQuestionsLocalCount);
        RpcCountdownStart(answeredQuestionsGlobalCount, currentQuestionData.answerDuration);
        yield return new WaitForSeconds(duration);

        /*foreach (GameObject currentWindow in currentQuestionData.questionWindows)
        {
            bool windowIsPoint = currentWindow.CompareTag(pointTag);
            bool windowIsAnswer = currentWindow.CompareTag(answerTag);

            questionWindow.SetActive(true);
            questionsGO[answeredQuestionsGlobalCount].SetActive(true);

            if (windowIsPoint)
            {
                currentWindow.SetActive(true);
                LeaderboardControl(true);
            }
            else if (windowIsAnswer)
            {
                //LeaderboardControl(false);
                answerWindowFinished = false;
                int timerTime = 0;

                while (timerTime < currentQuestionData.answerDuration*2 && !answerWindowFinished)
                {
                    yield return new WaitForSeconds(1f);
                    timerTime++;
                }
            }
            else
            {
                Debug.Log("Check tags!");
            }
            currentQuestionData.IncrementWindNum();
            currentWindow.SetActive(false);
        }*/

        answeredQuestionsGlobalCount++;
        currentQuestionData.Confirm();
        Debug.Log("Question " + currentQuestionData.number + " fade out");
        RpcQuestionPopUp();
    }

    [ClientRpc]
    private void RpcInitWindows(uint answeredQCount)
    {
        questionWindow.SetActive(true);
        questionsGO[answeredQCount].SetActive(true);

        if (!NetworkServer.active)
        {
            questionsGO[answeredQCount].GetComponent<QuestionData>().answerWindows[answeredQCount].SetActive(true);
        }
        else
        {
            currentQuestionData.pointWindows[0].SetActive(true);
            LeaderboardControl(true);
        }
    }

    [ClientRpc]
    private void RpcCountdownStart(uint QNum, float duration)
    {
        StartCoroutine(StartAnswerCountdown(QNum, duration));
    }

    private IEnumerator StartAnswerCountdown(uint QNum, float duration)
    {
        ResetTimer(true);
        answerDone = false;

        int timerTime = 0;
        timerActive = true;

        while (timerActive)
        {
            if (timerTime < duration && !answerDone /*|| !allAnswered*/)
            {
                yield return new WaitForSeconds(1);
                timerTime++;
                timerText.text = timerTime.ToString();
                timerFillImage.fillAmount += 1f / duration;
            }
            else
            {
                timerActive = false;

                yield return new WaitForSeconds(1f);
                
                ResetTimer(false);
                questionWindow.SetActive(false);

                // If Answer Button wasn't pressed
                if (!answerDone)
                {
                    currentQuestionData.FinishAnswerIfTimerRunsOut(currentQuestionData.number);
                }

                // MoveFigures
                MoveFigures(playerFigureTransform, playerData.GetPlayerScore());
                yield return new WaitForSeconds(5f);
                ScreenMessage(false);
                answerWindowFinished = true;

                yield break;
            }
        }
    }

    private void MoveFigures(Transform figureTransform, uint playerScore)
    {
        if (playerScore > 0)
        {
            if (playerScore > waypointsTransforms.Length)
            {
                var target = new Vector3(waypointsTransforms[waypointsTransforms.Length - 1].position.x, waypointsTransforms[waypointsTransforms.Length - 1].position.y, waypointsTransforms[waypointsTransforms.Length - 1].position.z);
                StartCoroutine(Movement(figureTransform, target, playerScore));
                //ScreenMessage(true, colorProcess, "Board limit reached\nWill move further, once board is sliced");
            }
            else
            {
                ScreenMessage(true, colorProcess, "Movement (Regarding to gained score)\nCurrentScore: " + playerData.GetPlayerScore());
                var target = new Vector3(waypointsTransforms[playerScore - 1].position.x, waypointsTransforms[playerScore - 1].position.y, waypointsTransforms[playerScore - 1].position.z);
                StartCoroutine(Movement(figureTransform, target, playerScore));
            }
        }
        else
        {
            ScreenMessage(true, colorError, "Stay on beginning\nCurrentScore: " + playerData.GetPlayerScore());
        }
    }

    private IEnumerator Movement(Transform figureTransform, Vector3 targetTransform, uint pScore)
    {
        const float closeEnough = 0.2f;
        float distance = (figureTransform.position - targetTransform).magnitude;

        var wait = new WaitForEndOfFrame();

        // Continue until we're there
        while (distance >= closeEnough)
        {
            // Move a bit then  wait until next  frame
            figureTransform.position = Vector3.Slerp(figureTransform.position, targetTransform, 0.01f);
            yield return wait;

            // Check if we should repeat
            distance = (figureTransform.position - targetTransform).magnitude;
        }

        // Complete the motion to prevent position mistakes
        figureTransform.position = targetTransform;
        var waypointRotation = waypointsTransforms[pScore - 1].rotation.eulerAngles;
        figureTransform.rotation = Quaternion.Euler(waypointRotation);

        Debug.Log("Movement complete");
    }

    private void LeaderboardControl(bool activeStatus)
    {
        leaderboard.SetActive(activeStatus);
        leaderboard.transform.GetChild(0).gameObject.GetComponent<Text>().text =
            playerData.GetPlayerScore() + " - " + playerData.GetPlayerName().Replace("Player ", string.Empty);
    }

    private void ScreenMessage(bool activeStatus, Color32 textColor, string text)
    {
        resultText.text = text;
        resultText.color = textColor;
        resultText.gameObject.SetActive(activeStatus);
    }

    private void ScreenMessage(bool activeStatus)
    {
        if (!activeStatus)
        {
            resultText.text = string.Empty;
            resultText.color = Color.white;
        }

        resultText.gameObject.SetActive(activeStatus);
    }

    public void ResetTimer(bool activeStatus)
    {
        timerObject.SetActive(activeStatus);
        timerText.text = "0";
        timerFillImage.fillAmount = 0f;
    }

    [ClientRpc]
    private void RpcTheEnd()
    {
        questionWindow.SetActive(false);
        ScreenMessage(true, colorSuccess, "The End");
        buttonExit.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
