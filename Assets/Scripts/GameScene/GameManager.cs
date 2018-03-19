using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int answeredQuestionsCount;
    private bool timerActive;
    private bool allAnswered;
    private bool answerWindowFinished;

    [HideInInspector]
    public bool answerDone;

    private Color32 colorProcess = new Color32(225, 216, 98, 255);
    private Color32 colorSuccess = new Color32(98, 225, 114, 255);
    private Color32 colorError = new Color32(225, 98, 98, 255);

    public PlayerData playerData;
    public Transform[] cameraPositionsTransforms;

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

    public void StartGame()
    {
        Debug.Log("GameManager started.");
        questionWindow.SetActive(false);
        buttonExit.SetActive(false);
        foreach (GameObject question in questionsGO)
        {
            question.SetActive(false);
        }

        playerData.Refresh();

        QuestionPopUp();
    }
    
    private void QuestionPopUp()
    {
        if (answeredQuestionsCount < questionsGO.Length)
        {
            // If there's still questions in list

            currentQuestionData = questionsGO[answeredQuestionsCount].GetComponent<QuestionData>();

            Debug.Log("Question: " + (currentQuestionData.number) +
                      ". MaxScore: " + currentQuestionData.scoreMaxValue +
                      ". Duration: " + currentQuestionData.answerDuration + " s");

            PointsControl();
        }
        else
        {
            // If questions are ended

            TheEnd();
        }
    }

    private void PointsControl()
    {
        StartCoroutine(WindowShow(5));
    }

    private void AnswerStart()
    {
        StartCoroutine(StartAnswerCountdown(currentQuestionData.answerDuration));
    }

    private IEnumerator WindowShow(uint durationPoint)
    {
        foreach (GameObject currentWindow in currentQuestionData.questionWindows)
        {
            bool windowIsPoint = currentWindow.CompareTag(pointTag);
            bool windowIsAnswer = currentWindow.CompareTag(answerTag);

            questionWindow.SetActive(true);
            questionsGO[answeredQuestionsCount].SetActive(true);

            currentWindow.SetActive(true);

            if (windowIsPoint)
            {
                LeaderboardControl(true);
                yield return new WaitForSeconds(durationPoint);
            }
            else if (windowIsAnswer)
            {
                LeaderboardControl(false);
                answerWindowFinished = false;
                Invoke("AnswerStart", 0f);
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
        }

        answeredQuestionsCount++;
        currentQuestionData.Confirm();
        Debug.Log("Question " + currentQuestionData.number + " fade out");
        QuestionPopUp();
    }

    private IEnumerator StartAnswerCountdown(float duration)
    {
        ResetTimer(true);
        answerDone = false;

        int timerTime = 0;
        timerActive = true;

        while (timerActive)
        {
            if (timerTime < duration && !answerDone)
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

                CameraMoveCheck();
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
                StartCoroutine(Movement(figureTransform, target));
                //ScreenMessage(true, colorProcess, "Board limit reached\nWill move further, once board is sliced");
            }
            else
            {
                ScreenMessage(true, colorProcess, "Movement (Regarding to gained score)\nCurrentScore: " + playerData.GetPlayerScore());
                var target = new Vector3(waypointsTransforms[playerScore - 1].position.x, waypointsTransforms[playerScore - 1].position.y, waypointsTransforms[playerScore - 1].position.z);
                StartCoroutine(Movement(figureTransform, target));
            }
        }
        else
        {
            ScreenMessage(true, colorError, "Stay on beginning\nCurrentScore: " + playerData.GetPlayerScore());
        }
    }

    private static IEnumerator Movement(Transform figureTransform, Vector3 targetTransform)
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

        Debug.Log("Movement complete");
    }

    private void CameraMoveCheck()
    {
        if (playerData.GetPlayerScore() >= 10 && playerData.GetPlayerScore() <= 20)
        {
            Camera.main.transform.position = new Vector3(cameraPositionsTransforms[0].position.x, cameraPositionsTransforms[0].position.y, cameraPositionsTransforms[0].position.z);
            Camera.main.transform.rotation = new Quaternion(cameraPositionsTransforms[0].rotation.x, cameraPositionsTransforms[0].rotation.y, cameraPositionsTransforms[0].rotation.z, cameraPositionsTransforms[0].rotation.w);
        }
        else if (playerData.GetPlayerScore() >= 20 && playerData.GetPlayerScore() <= 30)
        {
            Camera.main.transform.position = new Vector3(cameraPositionsTransforms[1].position.x, cameraPositionsTransforms[1].position.y, cameraPositionsTransforms[1].position.z);
            Camera.main.transform.rotation = new Quaternion(cameraPositionsTransforms[1].rotation.x, cameraPositionsTransforms[1].rotation.y, cameraPositionsTransforms[1].rotation.z, cameraPositionsTransforms[1].rotation.w);
        }
        else if ((playerData.GetPlayerScore() >= 30 && playerData.GetPlayerScore() <= 40))
        {
            Camera.main.transform.position = new Vector3(cameraPositionsTransforms[2].position.x, cameraPositionsTransforms[2].position.y, cameraPositionsTransforms[2].position.z);
            Camera.main.transform.rotation = new Quaternion(cameraPositionsTransforms[2].rotation.x, cameraPositionsTransforms[2].rotation.y, cameraPositionsTransforms[2].rotation.z, cameraPositionsTransforms[2].rotation.w);
        }
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

    private void TheEnd()
    {
        questionWindow.SetActive(false);
        ScreenMessage(true, colorSuccess, "End\n(More questions can be added fast)");
        buttonExit.SetActive(true);
        Debug.Log("Final!");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
