using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question8 : QuestionData
{
    [SerializeField]
    private Image answerImage;

    [SerializeField]
    private GameObject answerObject;

    private bool isAnsweringStarted;
    private bool rightAnswer;

    private uint timeSinceStart;

    private uint scoreValue;

    [SerializeField]
    private GameObject rightAnswerButton;

    private void Start()
    {
        answerImage.CrossFadeAlpha(0f, 0.01f, false);
    }

    private void Update()
    {
        if (!isAnsweringStarted && answerObject.activeSelf)
        {
            StartCoroutine(ImageShowing(answerDuration));
            isAnsweringStarted = true;
        }
    }

    private IEnumerator ImageShowing(uint duration)
    {
        timeSinceStart = 0;
        answerImage.CrossFadeAlpha(1.0f, duration, false);

        StartCoroutine(ScoreValueChange(duration));
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator ScoreValueChange(uint duration)
    {
        while (timeSinceStart < duration)
        {
            switch (timeSinceStart)
            {
                case 0:
                case 1:
                    scoreValue = scoreMaxValue;
                    Debug.Log("StartScore is: " + scoreValue + " !");
                    break;
                case 10:
                    scoreValue = scoreMaxValue - 1;
                    Debug.Log("Score is: " + scoreValue + " now!");
                    break;
                case 20:
                    scoreValue = scoreMaxValue - 2;
                    Debug.Log("Score is: " + scoreValue + " now!");
                    break;
                case 30:
                    scoreValue = scoreMaxValue - 3;
                    Debug.Log("Score is: " + scoreValue + " now!");
                    break;
                case 40:
                    scoreValue = scoreMaxValue - 4;
                    Debug.Log("Score is: " + scoreValue + " now!");
                    break;
            }

            yield return new WaitForSeconds(1f);
            timeSinceStart++;
        }
    }

    public void ButtonAnswerSelect(GameObject buttonClicked)
    {
        rightAnswer = buttonClicked == rightAnswerButton;

        Debug.Log("This (" + int.Parse(buttonClicked.name.Replace("Answer", string.Empty)) + ") answer is " + rightAnswer);
    }

    public void ButtonConfirmAnswer()
    {
        if (rightAnswer)
        {
            gameManager.playerData.AddPlayerScore(scoreValue);
        }

        Confirm();
    }
}
