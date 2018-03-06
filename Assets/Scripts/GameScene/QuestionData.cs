using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class QuestionData : MonoBehaviour
{
    public GameManager gameManager;

    public uint answerScoreValue;
    public uint answerCount;

    public uint duration = 60;

    [HideInInspector]
    public uint totalScoreValue;

    public uint number;

    [SerializeField]
    private GameObject[] questionWindows;

    private int windowNumber = 0;

    private uint scoreToAdd;

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
