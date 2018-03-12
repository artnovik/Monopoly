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
            StartCoroutine(ImageShowing(duration));
            isAnsweringStarted = true;
        }
    }

    private IEnumerator ImageShowing(uint duration)
    {
        timeSinceStart = 0;
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

            if (timeSinceStart < 10)
            {
                scoreValue = scoreMaxValue;
            }
            else if (timeSinceStart >= 10 && timeSinceStart < 20)
            {
                scoreValue = scoreMaxValue - 1;
            }
            else if (timeSinceStart >= 20 && timeSinceStart < 30)
            {
                scoreValue = scoreMaxValue - 2;
            }
            else if (timeSinceStart >= 30 && timeSinceStart < 40)
            {
                scoreValue = scoreMaxValue - 3;
            }
            else if (timeSinceStart >= 40 && timeSinceStart < 50)
            {
                scoreValue = scoreMaxValue - 4;
            }
            else
            {
                scoreValue = scoreMaxValue - scoreMaxValue + 1;
            }

            Debug.Log("Score is: " + scoreValue + " now!");

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

        foreach (var button in buttonsConfirmAnswer)
        {
            button.GetComponent<Button>().interactable = false;
        }

        gameManager.answerDone = true;

        gameObject.SetActive(false);
        gameManager.ResetTimer(false);
    }
}
