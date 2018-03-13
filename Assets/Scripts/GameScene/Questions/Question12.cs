using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question12 : QuestionData
{
    private bool answerRight;

    private uint scoreValue;

    [SerializeField]
    private GameObject rightAnswerButton;

    [SerializeField]
    private GameObject[] allAnswerButtons;

    public void ButtonAnswerSelect(GameObject buttonClicked)
    {
        answerRight = buttonClicked == rightAnswerButton;

        buttonClicked.GetComponent<Button>().interactable = false;

        foreach (var buttonGO in allAnswerButtons)
        {
            if (buttonGO != buttonClicked)
            {
                buttonGO.GetComponent<Button>().interactable = true;
            }
        }

        Debug.Log("This (" + int.Parse(buttonClicked.name.Replace("Answer", string.Empty)) + ") answer is " + answerRight);
    }

    public void ButtonConfirmAnswer()
    {
        if (answerRight)
        {
            scoreValue = scoreMaxValue;
            gameManager.playerData.AddPlayerScore(scoreValue);
        }

        foreach (var button in buttonsConfirmAnswer)
        {
            button.GetComponent<Button>().interactable = false;
        }

        gameManager.answerDone = true;

        gameObject.SetActive(false);
        gameManager.ResetTimer(false);
    }
}
