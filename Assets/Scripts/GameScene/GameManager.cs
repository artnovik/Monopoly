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

    [HideInInspector]
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

    private void Start()
    {
        Debug.Log("GameManager started.");
        questionWindow.SetActive(false);
        buttonExit.SetActive(false);
        foreach (GameObject question in questionsGO)
        {
            question.SetActive(false);
        }

        playerData.Refresh();
        StartGame();
    }

    private void Update()
    {
        // Host computer will only spectate
        //if (NetworkServer.active)
        //{
        //    return;
        //}
    }

    private void StartGame()
    {
        QuestionPopUp();
    }

    private void QuestionPopUp()
    {
        if (answeredQuestionsCount < questionsGO.Length)
        {
            // If there's still questions in list

            currentQuestionData = questionsGO[answeredQuestionsCount].GetComponent<QuestionData>();

            questionWindow.SetActive(true);


            questionsGO[answeredQuestionsCount].SetActive(true);
            Debug.Log("Question: " + (currentQuestionData.number) +
                      ". MaxScore: " + currentQuestionData.scoreMaxValue +
                      ". Duration: " + currentQuestionData.answerDuration + " s");

            PointsControl();
        }
        else
        {
            // If questions are ended

            questionWindow.SetActive(false);
            ScreenMessage(true, colorSuccess, "More questions on the way :)");
            buttonExit.SetActive(true);
            Debug.Log("Final!");
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

            currentWindow.SetActive(true);

            if (windowIsPoint)
            {
                LeaderboardControl(true);
                yield return new WaitForSeconds(durationPoint);
                // ToDo Second isn't appearing
            }
            else if (windowIsAnswer)
            {
                LeaderboardControl(false);
                Invoke("AnswerStart", 0f);
                // ToDo GoNExt (IMPORTANT) handle condition
                yield return new WaitForSeconds(currentQuestionData.answerDuration+1);
            }
            else
            {
                Debug.Log("Check tags!");
            }

            currentQuestionData.NextWindow();
            currentWindow.SetActive(false);
        }

        QuestionPopUp();
    }

    private IEnumerator StartAnswerCountdown(uint duration)
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

                yield return new WaitForSeconds(1);

                ResetTimer(false);
                questionWindow.SetActive(false);

                // If Answer Button wasn't pressed
                if (!answerDone)
                {
                    currentQuestionData.FinishAnswerIfTimerRunsOut(currentQuestionData.number);
                }

                // MoveFigures
                MoveFigures(playerFigureTransform, playerData.GetPlayerScore());
                Debug.Log("Question " + currentQuestionData.number + " fade out");
                answeredQuestionsCount++;
                yield return new WaitForSeconds(5);
                ScreenMessage(false);

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
                ScreenMessage(true, colorProcess, "Board limit reached\nWill move further, once board is sliced");
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
        const float closeEnough = 0.05f;
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
