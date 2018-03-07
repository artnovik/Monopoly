using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class QuestionData : MonoBehaviour
{
    public GameManager gameManager;

    public uint scoreMaxValue;

    public uint duration = 60;

    public uint number;

    [SerializeField]
    private GameObject[] questionWindows;

    public GameObject[] buttonsConfirmAnswer;

    protected int windowNumber;

    public void NextWindow()
    {
        gameObject.GetComponent<QuestionData>().questionWindows[windowNumber].SetActive(false);
        gameObject.GetComponent<QuestionData>().questionWindows[++windowNumber].SetActive(true);
    }

    public void FinishAnswerIfTimerRunsOut(uint questionNumber)
    {
        switch (questionNumber)
        {
            case 1:
                gameObject.GetComponent<Question1>().ButtonConfirmAnswer();
                break;
            case 3:
                gameObject.GetComponent<Question3>().ButtonConfirmAnswer();
                break;
            default:
                Debug.Log("Check smthn");
                break;
        }
    }
}
