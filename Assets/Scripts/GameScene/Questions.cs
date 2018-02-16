using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.Video;

public class Question
{
    protected uint scoreValue;
    protected uint rightAnswerNumber;
    //protected string rightAnswerText;
    //protected uint questionAnswersCount;
}


public class QuestionText : Question
{
    private string questionText;

    public QuestionText(uint _scoreValue, string _questionText, uint _rightAnswerNumber)
    {
        scoreValue = _scoreValue;
        questionText = _questionText;
        rightAnswerNumber = _rightAnswerNumber;
    }
}

public class QuestionImage : Question
{
    private Image questionImage;

    public QuestionImage(uint _scoreValue, Image _questionImage, uint _rightAnswerNumber)
    {
        scoreValue = _scoreValue;
        questionImage = _questionImage;
        rightAnswerNumber = _rightAnswerNumber;
    }
}

public class QuestionVideo : Question
{
    private VideoClip questionVideo;

    public QuestionVideo(uint _scoreValue, VideoClip _questionVideo, uint _rightAnswerNumber)
    {
        scoreValue = _scoreValue;
        questionVideo = _questionVideo;
        rightAnswerNumber = _rightAnswerNumber;
    }
}
