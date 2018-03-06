using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class QuestionData : MonoBehaviour
{
    public GameManager gameManager;

    public uint answerScoreValue;
    public uint answerCount;

    public uint duration = 60;

    [HideInInspector]
    public uint totalScoreValue;

    public uint number;

    [SerializeField]
    private GameObject[] questionWindows;

    private int windowNumber = 0;

    private uint scoreToAdd;

    public void NextWindow()
    {
        gameObject.GetComponent<QuestionData>().questionWindows[windowNumber].SetActive(false);
        gameObject.GetComponent<QuestionData>().questionWindows[++windowNumber].SetActive(true);
    }

    public void FinishAnswerIfTimerRunsOut(int questionNumber)
    {
        switch (questionNumber)
        {
            case 1:
                gameObject.GetComponent<Question1>().ButtonConfirmAnswerFinishIfTimer();
                break;
            case 2:
                gameObject.GetComponent<Question3>().ButtonConfirmAnswerFinishIfTimer();
                break;
            default:
                Debug.Log("Check smthn");
                break;
        }
    }

    public void Refresh()
    {
        totalScoreValue = answerScoreValue * answerCount;
    }
}
