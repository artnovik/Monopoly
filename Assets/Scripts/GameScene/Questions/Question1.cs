using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question1 : QuestionData
{
    private int clickNum;
    private string firstClickNumber;
    private string secondClickNumber;
    private bool firstClickDone;

    private GameObject buttonClickedContainer;

    private uint scoreValue;

    private Color32 colorAnswer1 = new Color32(98, 225, 114, 255);
    private Color32 colorAnswer2 = new Color32(98, 173, 225, 255);
    private Color32 colorAnswer3 = new Color32(219, 225, 98, 255);
    private Color32 colorAnswer4 = new Color32(225, 98, 212, 255);
    private Color32 colorAnswer5 = new Color32(216, 216, 216, 255);

    public void ButtonImageSelect(GameObject buttonClicked)
    {
        buttonClicked.GetComponent<Button>().interactable = false;

        if (!firstClickDone)
        {
            clickNum++;
            firstClickDone = true;
            firstClickNumber = buttonClicked.name.Replace("Answer", string.Empty);
            Debug.Log(firstClickNumber + " is Clicked!");

            buttonClicked.transform.Find("AnswerNumberText").gameObject.GetComponent<Text>().text = clickNum.ToString();
            Colorizing(buttonClicked.transform.Find("AnswerNumberText").gameObject.GetComponent<Text>());
        }
        else
        {
            firstClickDone = false;
            secondClickNumber = buttonClicked.name.Replace("Answer", string.Empty);
            if (firstClickNumber == secondClickNumber)
            {
                scoreValue += scoreMaxValue / 5;
                Debug.Log("Right! Player will gain + " + scoreMaxValue / 5 + ". Total gain: " + scoreValue);
            }
            else
            {
                Debug.Log("Error!");
            }

            buttonClicked.transform.Find("AnswerNumberText").gameObject.GetComponent<Text>().text = clickNum.ToString();
            Colorizing(buttonClicked.transform.Find("AnswerNumberText").gameObject.GetComponent<Text>());
        }
    }

    public void ButtonConfirmAnswer()
    {
        gameManager.playerData.AddPlayerScore(scoreValue);

        Confirm();
    }

    private void Colorizing(Text answerNumText)
    {
        switch (answerNumText.text)
        {
            case "1":
                answerNumText.color = colorAnswer1;
                break;
            case "2":
                answerNumText.color = colorAnswer2;
                break;
            case "3":
                answerNumText.color = colorAnswer3;
                break;
            case "4":
                answerNumText.color = colorAnswer4;
                break;
            case "5":
                answerNumText.color = colorAnswer5;
                break;
            default:
                Debug.Log("Check code");
                break;
        }
    }
}
