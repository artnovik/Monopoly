using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class QuestionData : MonoBehaviour
{
    [Header("QuestionData")]
    public GameManager gameManager;

    public uint scoreMaxValue;

    public uint answerDuration = 60;

    public uint number;

    public GameObject[] questionWindows;

    public GameObject[] buttonsConfirmAnswer;

    protected int windowNumber;

    public void NextWindow()
    {
        //if (questionWindows != null)
        //{
        //    questionWindows[windowNumber].SetActive(false);
        //    questionWindows[++windowNumber].SetActive(true);
        //}
        ++windowNumber;
    }

    public void FinishAnswerIfTimerRunsOut(uint questionNumber)
    {
        switch (questionNumber)
        {
            case 1:
                gameObject.GetComponent<Question1>().ButtonConfirmAnswer();
                break;
            case 2:
                // Temp
                gameObject.GetComponent<Question2>().ButtonFirstConfirmAnswer();
                break;
            case 3:
                gameObject.GetComponent<Question3>().ButtonConfirmAnswer();
                break;
            case 4:
                // Temp
                gameObject.GetComponent<Question4>().ButtonFirstConfirmAnswer();
                break;
            case 5:
                gameObject.GetComponent<Question5>().ButtonConfirmAnswer();
                break;
            case 6:
                gameObject.GetComponent<Question6>().ButtonConfirmAnswer();
                break;
            case 8:
                gameObject.GetComponent<Question8>().ButtonConfirmAnswer();
                break;
            case 10:
                // Temp
                gameObject.GetComponent<Question10>().ButtonFirstConfirmAnswer();
                break;
            case 12:
                gameObject.GetComponent<Question12>().ButtonConfirmAnswer();
                break;
            case 13:
                gameObject.GetComponent<Question13>().ButtonConfirmAnswer();
                break;
            case 17:
                gameObject.GetComponent<Question17>().ButtonConfirmAnswer();
                break;
            default:
                Debug.Log("You're passing nonexistent question number");
                break;
        }
    }
}
