using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Question2 : QuestionData
{
    private bool firstAnswerRight;

    private bool secondAnswerRight;

    private bool thirdAnswerRight;

    private uint scoreValue;

    private uint answeredRightCountFirst;
    private uint answeredRightCountSecond;
    private uint answeredRightCountThird;

    [SerializeField]
    private GameObject[] allAnswerButtonsFirst;

    [SerializeField]
    private GameObject[] allAnswerButtonsSecond;

    [SerializeField]
    private GameObject[] allAnswerButtonsThird;

    private const uint maxAllowedClicks = 3;

    private uint clickedButtonsCountFirst;
    private uint clickedButtonsCountSecond;
    private uint clickedButtonsCountThird;

    [SerializeField]
    private GameObject[] firstRightAnswerButtons;

    [SerializeField]
    private GameObject[] secondRightAnswerButtons;

    [SerializeField]
    private GameObject[] thirdRightAnswerButtons;

    public void ButtonFirstAnswerSelect(GameObject buttonFirstClicked)
    {
        if (clickedButtonsCountFirst < maxAllowedClicks)
        {
            clickedButtonsCountFirst++;

            buttonFirstClicked.GetComponent<Button>().interactable = false;

            if (firstRightAnswerButtons.Contains(buttonFirstClicked))
            {
                answeredRightCountFirst++;
                if (answeredRightCountFirst == scoreMaxValue)
                {
                    firstAnswerRight = true;
                }
            }

            if (clickedButtonsCountFirst == maxAllowedClicks)
            {
                foreach (var buttonGO in allAnswerButtonsFirst)
                {
                    if (buttonGO.GetComponent<Button>().interactable)
                    {
                        buttonGO.SetActive(false);
                    }
                }
            }
        }
    }

    public void ButtonSecondAnswerSelect(GameObject buttonSecondClicked)
    {
        if (clickedButtonsCountSecond < maxAllowedClicks)
        {
            clickedButtonsCountSecond++;

            buttonSecondClicked.GetComponent<Button>().interactable = false;

            if (secondRightAnswerButtons.Contains(buttonSecondClicked))
            {
                answeredRightCountSecond++;
                if (answeredRightCountSecond == scoreMaxValue)
                {
                    secondAnswerRight = true;
                }
            }

            if (clickedButtonsCountSecond == maxAllowedClicks)
            {
                foreach (var buttonGO in allAnswerButtonsSecond)
                {
                    if (buttonGO.GetComponent<Button>().interactable)
                    {
                        buttonGO.SetActive(false);
                    }
                }
            }
        }
    }

    public void ButtonThirdAnswerSelect(GameObject buttonThirdClicked)
    {
        if (clickedButtonsCountThird < maxAllowedClicks)
        {
            clickedButtonsCountThird++;

            buttonThirdClicked.GetComponent<Button>().interactable = false;

            if (thirdRightAnswerButtons.Contains(buttonThirdClicked))
            {
                answeredRightCountThird++;
                if (answeredRightCountThird == scoreMaxValue)
                {
                    thirdAnswerRight = true;
                }
            }

            if (clickedButtonsCountThird == maxAllowedClicks)
            {
                foreach (var buttonGO in allAnswerButtonsThird)
                {
                    if (buttonGO.GetComponent<Button>().interactable)
                    {
                        buttonGO.SetActive(false);
                    }
                }
            }
        }
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
            case 2:
                if (thirdAnswerRight)
                {
                    scoreValue = scoreMaxValue / 2;
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
