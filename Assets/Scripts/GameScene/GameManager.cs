using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int answeredQuestionsCount;
    private bool timerActive;
    private bool allAnswered;
    private bool answered;

    [SerializeField]
    private PlayerInfo playerInfo;

    [Header("Question Window")]

    [SerializeField]
    private GameObject questionWindow;

    [SerializeField]
    private Text questionNumberText;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private GameObject[] answers_GOs;

    [Header("Timer")]

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Image timerFillImage;

    private void Start()
    {
        Debug.Log("GameManager started.");

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
        QuestionPopUp(answeredQuestionsCount);
    }

    private void QuestionPopUp(int questionNumber)
    {
        ClearWindow();

        if (questionNumber < QuestionsList.questionsList.Count)
        {
            FillWindow();
            questionWindow.SetActive(true);

            Debug.Log("Question: " + (answeredQuestionsCount + 1) +
                      ". ScoreValue: " + QuestionsList.questionsList[questionNumber].scoreValue +
                      ". Right answer is: " + QuestionsList.questionsList[questionNumber].rightAnswerNumber +
                      ". Duration: " + QuestionsList.questionsList[questionNumber].duration);

            timerActive = true;
            StartCoroutine(StartTimer(QuestionsList.questionsList[questionNumber].duration));
        }
        else
        {
            questionWindow.SetActive(false);
            Debug.Log("Final!");
        }
    }

    private IEnumerator StartTimer(int duration)
    {
        int timerTime = 0;
        timerActive = true;

        while (timerActive)
        {
            // Question End
            if (timerTime < duration /*|| !allAnswered || !answered*/)
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
                questionWindow.SetActive(false);
                // MoveFigures();
                Debug.Log("Question fade out");
                answeredQuestionsCount++;
                yield return new WaitForSeconds(5);
                QuestionPopUp(answeredQuestionsCount);
                yield break;
            }
        }
    }

    private void ClearWindow()
    {
        timerText.text = string.Empty;
        questionText.text = string.Empty;
        questionNumberText.text = string.Empty;
        timerFillImage.fillAmount = 0f;
    }

    private void FillWindow()
    {
        questionNumberText.text = (answeredQuestionsCount + 1).ToString();
        questionText.text = QuestionsList.questionsList[answeredQuestionsCount].questionText;
        timerText.text = "0";

        // ToDo Answers
    }

    public void CheckAnswerClick(GameObject clickedAnswer)
    {
        // Badass logic

        for (int i = 0; i < answers_GOs.Length; i++)
        {
            if ((answers_GOs[i] == clickedAnswer) && (i + 1) == QuestionsList.questionsList[answeredQuestionsCount].rightAnswerNumber)
            {
                Debug.Log("Correct Answer!");
                playerInfo.AddPlayerScore(QuestionsList.questionsList[answeredQuestionsCount].scoreValue);

                // ToDo QWindow disappears and message "Correct!" appears for X seconds.

            }
            else if ((answers_GOs[i] == clickedAnswer) && (i + 1) != QuestionsList.questionsList[answeredQuestionsCount].rightAnswerNumber)
            {
                Debug.Log("Incorrect Answer!");

                // ToDo QWindow disappears and message "Incorrect!" appears for X seconds.
            }

            // ToDo In any case: Waiting for allAnswered OR timerEnded. Then Next question.
        }
    }
}
