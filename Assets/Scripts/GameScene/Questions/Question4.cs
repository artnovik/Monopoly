using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question4 : QuestionData
{
    private bool firstAnswerRight;

    private bool secondAnswerRight;

    private uint scoreValue;

    [SerializeField]
    private GameObject firstRightAnswerButton;

    [SerializeField]
    private GameObject secondRightAnswerButton;

    public void ButtonFirstAnswerSelect(GameObject buttonFirstClicked)
    {
        firstAnswerRight = buttonFirstClicked == firstRightAnswerButton;

        Debug.Log("This (" + int.Parse(buttonFirstClicked.name.Replace("Answer", string.Empty)) + ") answer is " + firstAnswerRight);
    }

    public void ButtonSecondAnswerSelect(GameObject buttonSecondClicked)
    {
        secondAnswerRight = buttonSecondClicked == secondRightAnswerButton;

        Debug.Log("This (" + int.Parse(buttonSecondClicked.name.Replace("Answer", string.Empty)) + ") answer is " + secondAnswerRight);
    }

    public void ButtonFirstConfirmAnswer()
    {
        if (firstAnswerRight)
        {
            scoreValue = scoreMaxValue / scoreMaxValue;
            gameManager.playerData.AddPlayerScore(scoreValue);
        }

        buttonsConfirmAnswer[windowNumber].GetComponent<Button>().interactable = false;

        gameManager.answerDone = true;

        gameObject.SetActive(false);
        gameManager.ResetTimer(false);
    }

    public void ButtonSecondConfirmAnswer()
    {
        if (secondAnswerRight)
        {
            scoreValue = scoreMaxValue / scoreMaxValue;
            gameManager.playerData.AddPlayerScore(scoreValue);
        }

        buttonsConfirmAnswer[windowNumber].GetComponent<Button>().interactable = false;
        gameManager.answerDone = true;

        gameObject.SetActive(false);
        gameManager.ResetTimer(false);
    }
}
