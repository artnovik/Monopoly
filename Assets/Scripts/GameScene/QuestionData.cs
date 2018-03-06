using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class QuestionData : MonoBehaviour
{
    public int answerScoreValue;
    public int answerCount;

    public int duration = 60;

    [HideInInspector]
    public int totalScoreValue;

    public int number;

    [SerializeField]
    private GameObject[] questionWindows;

    private int windowNumber = 0;

    private int scoreToAdd;

    public void NextWindow()
    {
        gameObject.GetComponent<QuestionData>().questionWindows[windowNumber].SetActive(false);
        gameObject.GetComponent<QuestionData>().questionWindows[++windowNumber].SetActive(true);
    }

    public void FinishAnswer()
    {
        Debug.Log("Finish");
    }

    public void Refresh()
    {
        totalScoreValue = answerScoreValue * answerCount;
    }
}
