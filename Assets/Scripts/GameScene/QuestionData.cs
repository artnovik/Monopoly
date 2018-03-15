using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class QuestionData : MonoBehaviour
{
    [Header("QuestionData")]
    public GameManager gameManager;

    public uint scoreMaxValue;

    public uint answerDuration = 60;

    public uint number;

    public GameObject[] questionWindows;

    public GameObject[] buttonsConfirmAnswer;

    protected uint windowNumber;
    protected uint confirmButtonsClickCount;

    public void IncrementWindNum()
    {
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
                gameObject.GetComponent<Question2>().ButtonConfirmAnswer();
                break;
            case 3:
                gameObject.GetComponent<Question3>().ButtonConfirmAnswer();
                break;
            case 4:
                gameObject.GetComponent<Question4>().ButtonConfirmAnswer();
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
                gameObject.GetComponent<Question10>().ButtonConfirmAnswer();
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

    public void Confirm()
    {
        if (buttonsConfirmAnswer.Length > confirmButtonsClickCount)
        {
            buttonsConfirmAnswer[confirmButtonsClickCount].GetComponent<Button>().interactable = false;
            gameManager.answerDone = true;
            gameManager.ResetTimer(false);
            confirmButtonsClickCount++;
        }

        gameObject.SetActive(false);
    }
}
