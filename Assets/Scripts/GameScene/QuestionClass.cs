using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuestionClass
{
    public uint scoreValue;
    public uint rightAnswerNumber;
    public string questionText;
    public int duration;

    //protected string rightAnswerText;
    //protected uint questionAnswersCount;
}

public class QuestionText : QuestionClass
{
    public QuestionText(uint _scoreValue, string _questionText, uint _rightAnswerNumber, int _duration)
    {
        scoreValue = _scoreValue;
        rightAnswerNumber = _rightAnswerNumber;
        questionText = _questionText;
        duration = _duration;
    }
}

public class QuestionImage : QuestionClass
{
    public Image questionImage;

    public QuestionImage(uint _scoreValue, Image _questionImage, uint _rightAnswerNumber, int _duration)
    {
        scoreValue = _scoreValue;
        rightAnswerNumber = _rightAnswerNumber;
        questionImage = _questionImage;
        duration = _duration;
    }
}

public class QuestionVideo : QuestionClass
{
    public VideoClip questionVideo;

    public QuestionVideo(uint _scoreValue, VideoClip _questionVideo, uint _rightAnswerNumber, int _duration)
    {
        scoreValue = _scoreValue;
        rightAnswerNumber = _rightAnswerNumber;
        questionVideo = _questionVideo;
        duration = _duration;
    }
}

public class QuestionsList
{
    public static QuestionText question1 = new QuestionText(5, "Question_One", 2, 5);
    public static QuestionText question2 = new QuestionText(2, "Question_Two", 4, 7);
    public static QuestionText question3 = new QuestionText(10, "Question_Three", 1, 10);
    public static QuestionText question4 = new QuestionText(4, "Question_Four", 2, 6);
    public static QuestionText question5 = new QuestionText(7, "Question_Five", 3, 8);

    public static List<QuestionClass> questionsList = new List<QuestionClass>()
    {
        question1,
        question2,
        question3,
        question4,
        question5
    };
}
