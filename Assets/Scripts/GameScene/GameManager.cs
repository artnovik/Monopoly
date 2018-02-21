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

    [Header("Question Window")]

    [SerializeField]
    private GameObject questionWindow;

    [SerializeField]
    private Text questionNumberText;

    [SerializeField]
    private Text questionText;

    [Header("Timer")]

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private Image timerFillImage;

    private void Start()
    {
        Debug.Log("GameManager started.");

        // ToDo Question Mechanics
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
        // ToDo Answers
        // Answers
    }
}
