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
    public bool answered;

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
        // If there's still questions in list
        if (answeredQuestionsCount < questionsGO.Length)
        {
            currentQuestionData = questionsGO[answeredQuestionsCount].GetComponent<QuestionData>();

            questionWindow.SetActive(true);
            questionsGO[answeredQuestionsCount].SetActive(true);

            Debug.Log("Question: " + (currentQuestionData.number) +
                      ". MaxScore: " + currentQuestionData.scoreMaxValue +
                      ". Duration: " + currentQuestionData.duration + " s");

            Invoke("AnswerStart", 5f);
        }
        // If questions are ended
        else
        {
            questionWindow.SetActive(false);
            ToastMessage(true, "Ending will be here, after all questions\nBut you see how mechanics works");
            buttonExit.SetActive(true);
            Debug.Log("Final!");
        }
    }

    private void AnswerStart()
    {
        StartCoroutine(StartTimer(currentQuestionData.duration));
    }

    private IEnumerator StartTimer(uint duration)
    {
        ResetTimer();
        timerObject.SetActive(true);
        answered = false;

        int timerTime = 0;
        timerActive = true;

        currentQuestionData.NextWindow();

        while (timerActive)
        {
            if (timerTime < duration && !answered /*|| !allAnswered*/)
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

                questionsGO[answeredQuestionsCount].SetActive(false);
                timerObject.SetActive(false);
                questionWindow.SetActive(false);

                // If Answer Button wasn't pressed
                if (!answered)
                {
                    currentQuestionData.FinishAnswerIfTimerRunsOut(currentQuestionData.number);
                }

                // MoveFigures
                MoveFigures(playerFigureTransform, playerData.GetPlayerScore());
                Debug.Log("Question fade out");
                answeredQuestionsCount++;
                yield return new WaitForSeconds(5);
                ToastMessage(false);

                QuestionPopUp();
                yield break;
            }
        }
    }

    private void MoveFigures(Transform figureTransform, uint playerScore)
    {
        ToastMessage(true, "Movement (Regarding to gained score)\nCurrentScore: " + playerData.GetPlayerScore());

        if (playerScore > 0)
        {
            var target = new Vector3(waypointsTransforms[playerScore - 1].position.x, waypointsTransforms[playerScore - 1].position.y, waypointsTransforms[playerScore - 1].position.z);
            StartCoroutine(Movement(figureTransform, target));
        }
    }

    private static IEnumerator Movement(Transform figureTransform, Vector3 targetTransform)
    {
        // Will need to perform some of this process and yield until next frames
        const float closeEnough = 0.05f;
        float distance = (figureTransform.position - targetTransform).magnitude;

        // GC will trigger unless we define this ahead of time
        var wait = new WaitForEndOfFrame();

        Debug.Log("Score is > 0. We are moving.");

        // Continue until we're there
        while (distance >= closeEnough)
        {
            // Confirm that it's moving
            //Debug.Log("Executing Movement");

            // Move a bit then  wait until next  frame
            figureTransform.position = Vector3.Slerp(figureTransform.position, targetTransform, 0.01f);
            yield return wait;

            // Check if we should repeat
            distance = (figureTransform.position - targetTransform).magnitude;
        }

        // Complete the motion to prevent negligible sliding
        figureTransform.position = targetTransform;

        // Confirm  it's ended
        Debug.Log("Movement Complete");
    }

    private void ToastMessage(bool activeStatus, string text)
    {
        resultText.text = text;
        resultText.gameObject.SetActive(activeStatus);
    }

    private void ToastMessage(bool activeStatus)
    {
        if (!activeStatus)
        {
            resultText.text = string.Empty;
        }

        resultText.gameObject.SetActive(activeStatus);
    }

    private void ResetTimer()
    {
        timerText.text = "0";
        timerFillImage.fillAmount = 0f;
    }

    // MessageResult/CheckAnswerClick
    //private IEnumerator MessageResult(bool answerResultIsTrue)
    //{
    //    const float delay = 3f;

    //    answered = true;
    //    questionWindow.SetActive(false);

    //    if (answerResultIsTrue)
    //    {
    //        resultText.color = colorSuccess;
    //        ToastMessage(true, "Correct!");
    //    }
    //    else
    //    {
    //        resultText.color = colorError;
    //        ToastMessage(true, "Wrong!");
    //    }

    //    yield return new WaitForSeconds(delay);
    //    ToastMessage(false);
    //}

    //public void CheckAnswerClick(GameObject clickedAnswer)
    //{
    //    // Badass logic
    //    for (int i = 0; i < answers_GOs.Length; i++)
    //    {
    //        if ((answers_GOs[i] == clickedAnswer) && (i + 1) == QuestionsList.questionsList[answeredQuestionsCount].rightAnswerNumber)
    //        {
    //            Debug.Log("Correct Answer!");
    //            playerData.AddPlayerScore(QuestionsList.questionsList[answeredQuestionsCount].scoreMaxValue);
    //            StartCoroutine(MessageResult(true));
    //        }
    //        else if ((answers_GOs[i] == clickedAnswer) && (i + 1) != QuestionsList.questionsList[answeredQuestionsCount].rightAnswerNumber)
    //        {
    //            Debug.Log("Incorrect Answer!");
    //            StartCoroutine(MessageResult(false));
    //        }

    //        // ToDo In any case: Waiting for allAnswered OR timerEnded. Then Next question.
    //    }
    //}

    public void ExitGame()
    {
        Application.Quit();
    }
}
