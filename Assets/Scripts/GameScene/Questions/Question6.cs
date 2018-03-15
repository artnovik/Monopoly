using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Question6 : QuestionData
{
    private uint scoreValue;

    private uint answeredRightCount;

    [SerializeField]
    private GameObject[] allAnswerButtons;

    private const uint maxAllowedClicks = 7;

    private uint clickedButtonsCount;

    [SerializeField]
    private GameObject[] rightAnswerButtons;

    public void ButtonAnswerSelect(GameObject buttonClicked)
    {
        if (clickedButtonsCount < maxAllowedClicks)
        {
            clickedButtonsCount++;

            buttonClicked.GetComponent<Button>().interactable = false;

            if (rightAnswerButtons.Contains(buttonClicked))
            {
                answeredRightCount++;
            }

            if (clickedButtonsCount == maxAllowedClicks)
            {
                foreach (var buttonGO in allAnswerButtons)
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
        scoreValue = answeredRightCount;
        gameManager.playerData.AddPlayerScore(scoreValue);

        Confirm();
    }
}
