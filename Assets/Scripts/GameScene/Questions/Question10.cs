using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question10 : QuestionData
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

    public void ButtonConfirmAnswer()
    {
        switch (confirmButtonsClickCount)
        {
            case 0:
                if (firstAnswerRight)
                {
                    scoreValue = scoreMaxValue / scoreMaxValue;
                    gameManager.playerData.AddPlayerScore(scoreValue);
                }
                break;
            case 1:
                if (secondAnswerRight)
                {
                    scoreValue = scoreMaxValue / scoreMaxValue;
                    gameManager.playerData.AddPlayerScore(scoreValue);
                }
                break;
            default:
                Debug.Log("Add more cases!");
                break;
        }

        Confirm();
    }
}
