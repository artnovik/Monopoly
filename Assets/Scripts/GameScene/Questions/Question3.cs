using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question3 : MonoBehaviour
{
    [SerializeField]
    private Image answerImage;

    [SerializeField]
    private GameObject answerObject;

    [SerializeField]
    private QuestionData question3Data;

    private bool isAnsweringStarted;
    private bool rightAnswer;

    private uint timeSinceStart;

    private uint scoreValue;

    private int rightAnswerNumber = 4;

    private void Update()
    {
        if (!isAnsweringStarted && answerObject.activeSelf)
        {
            StartCoroutine(ImageShowing(question3Data.duration));
            isAnsweringStarted = true;
        }
    }

    private IEnumerator ImageShowing(uint duration)
    {
        timeSinceStart = 0;
        answerImage.CrossFadeAlpha(0f, 0.01f, false);
        answerImage.CrossFadeAlpha(1.0f, duration, false);

        StartCoroutine(ScoreValueChange(duration));
        yield return new WaitForSeconds(duration);

        Debug.Log("Ending");
    }

    private IEnumerator ScoreValueChange(uint duration)
    {
        while (timeSinceStart < duration)
        {
            yield return new WaitForSeconds(1f);
            timeSinceStart++;

            if (timeSinceStart < 10)
            {
                scoreValue = question3Data.answerScoreValue;
                Debug.Log(scoreValue.ToString());
            }
            else if (timeSinceStart >= 10 && timeSinceStart < 20)
            {
                scoreValue = question3Data.answerScoreValue - 1;
                Debug.Log(scoreValue.ToString());
            }
            else if (timeSinceStart >= 20 && timeSinceStart < 30)
            {
                scoreValue = question3Data.answerScoreValue - 2;
                Debug.Log(scoreValue.ToString());
            }
            else if (timeSinceStart >= 30 && timeSinceStart < 40)
            {
                scoreValue = question3Data.answerScoreValue - 3;
                Debug.Log(scoreValue.ToString());
            }
            else if (timeSinceStart >= 40 && timeSinceStart < 50)
            {
                scoreValue = question3Data.answerScoreValue - 4;
                Debug.Log(scoreValue.ToString());
            }
            else
            {
                scoreValue = question3Data.answerScoreValue - question3Data.answerScoreValue + 1;
                Debug.Log("Score is: " + scoreValue + " now!");
            }
        }
    }

    public void ButtonAnswerSelect(GameObject buttonClicked)
    {
        int clickedAnswerNumber = Int32.Parse(buttonClicked.name.Replace("Answer", string.Empty));
        Debug.Log(clickedAnswerNumber.ToString());

        rightAnswer = clickedAnswerNumber == rightAnswerNumber;

        Debug.Log("This (" + clickedAnswerNumber + ") answer is " + rightAnswer);
    }

    public void ButtonConfirmAnswer(GameObject buttonConfirm)
    {

        if (rightAnswer)
        {
            question3Data.gameManager.playerData.AddPlayerScore(scoreValue);
        }

        buttonConfirm.GetComponent<Button>().interactable = false;
        question3Data.gameManager.answered = true;
    }

    public void ButtonConfirmAnswerFinishIfTimer()
    {
        if (rightAnswer)
        {
            question3Data.gameManager.playerData.AddPlayerScore(scoreValue);
        }

        question3Data.gameManager.answered = true;
    }
}
