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

    private int answeredRightCountFirst;
    private int answeredRightCountSecond;
    private int answeredRightCountThird;

    [SerializeField]
    private GameObject[] firstRightAnswerButtons;

    [SerializeField]
    private GameObject[] secondRightAnswerButtons;

    [SerializeField]
    private GameObject[] thirdRightAnswerButtons;

    private Color pressedColor = new Color32(225, 255, 255, 160);

    public void ButtonFirstAnswerSelect(GameObject buttonFirstClicked)
    {
        buttonFirstClicked.GetComponent<Button>().interactable = false;
        //ColorBlock cb = buttonFirstClicked.GetComponent<Button>().colors;
        //cb.normalColor = pressedColor;
        //buttonFirstClicked.GetComponent<Button>().colors = cb;

        if (firstRightAnswerButtons.Contains(buttonFirstClicked))
        {
            answeredRightCountFirst++;
            if (answeredRightCountFirst == scoreMaxValue)
            {
                firstAnswerRight = true;
            }
        }
    }

    public void ButtonSecondAnswerSelect(GameObject buttonSecondClicked)
    {
        buttonSecondClicked.GetComponent<Button>().interactable = false;
        //ColorBlock cb = buttonFirstClicked.GetComponent<Button>().colors;
        //cb.normalColor = pressedColor;
        //buttonFirstClicked.GetComponent<Button>().colors = cb;

        if (secondRightAnswerButtons.Contains(buttonSecondClicked))
        {
            answeredRightCountSecond++;
            if (answeredRightCountSecond == scoreMaxValue)
            {
                secondAnswerRight = true;
            }
        }
    }

    public void ButtonThirdAnswerSelect(GameObject buttonThirdClicked)
    {
        buttonThirdClicked.GetComponent<Button>().interactable = false;
        //ColorBlock cb = buttonFirstClicked.GetComponent<Button>().colors;
        //cb.normalColor = pressedColor;
        //buttonFirstClicked.GetComponent<Button>().colors = cb;

        if (thirdRightAnswerButtons.Contains(buttonThirdClicked))
        {
            answeredRightCountThird++;
            if (answeredRightCountThird == scoreMaxValue)
            {
                thirdAnswerRight = true;
            }
        }
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

    public void ButtonThirdConfirmAnswer()
    {
        if (thirdAnswerRight)
        {
            scoreValue = scoreMaxValue / 2;
            gameManager.playerData.AddPlayerScore(scoreValue);
        }

        buttonsConfirmAnswer[windowNumber].GetComponent<Button>().interactable = false;
        gameManager.answerDone = true;

        gameObject.SetActive(false);
        gameManager.ResetTimer(false);
    }
}
