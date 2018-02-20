using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private uint answeredQuestionsCount;
    private bool timerActive;

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
        QuestionPopUp();
    }

    private void QuestionPopUp()
    {
        ClearWindow();
        FillWindow(answeredQuestionsCount);
        questionWindow.SetActive(true);
        timerActive = true;
        StartCoroutine(StartTimer(QuestionsList.question1.duration));
    }

    private IEnumerator StartTimer(int duration)
    {
        int timerTime = 0;
        while (timerActive)
        {
            yield return new WaitForSeconds(1);
            timerTime++;
            timerText.text = timerTime.ToString();
            timerFillImage.fillAmount += 1f / duration;

            if (timerTime == duration /*|| allAnswered*/)
            {
                timerActive = false;
                answeredQuestionsCount++;
                yield return new WaitForSeconds(1);
                // MoveFigures();
                Debug.Log("Question fade out");
                questionWindow.SetActive(false);
                StopCoroutine(StartTimer(QuestionsList.question1.duration));
            }
        }
    }

    private void ClearWindow()
    {
        timerText.text = string.Empty;
        questionText.text = string.Empty;
        questionNumberText.text = string.Empty;
    }

    private void FillWindow(uint _answeredQuestionsCount)
    {
        uint currentQuestionNumber = ++_answeredQuestionsCount;
        questionNumberText.text = currentQuestionNumber.ToString();
        questionText.text = QuestionsList.question1.questionText;
    }

    private void ConstructQuestion()
    {
        // ToDo Generate custom question
    }
}
