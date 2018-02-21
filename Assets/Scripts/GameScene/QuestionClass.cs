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
    public QuestionText(uint _scoreValue, string _questionText, int _duration)
    {
        scoreValue = _scoreValue;
        questionText = _questionText;
        duration = _duration;
    }
}

public class QuestionImage : QuestionClass
{
    public Image questionImage;

    public QuestionImage(uint _scoreValue, Image _questionImage, int _duration)
    {
        scoreValue = _scoreValue;
        questionImage = _questionImage;
        duration = _duration;
    }
}

public class QuestionVideo : QuestionClass
{
    public VideoClip questionVideo;

    public QuestionVideo(uint _scoreValue, VideoClip _questionVideo, int _duration)
    {
        scoreValue = _scoreValue;
        questionVideo = _questionVideo;
        duration = _duration;
    }
}

// ToDo Array stuff
public class QuestionsList
{
    public static QuestionText question1 = new QuestionText(5, "Question_One", 5);
    public static QuestionText question2 = new QuestionText(2, "Question_Two", 7);

    public static List<QuestionClass> questionsList = new List<QuestionClass>()
    {
        question1,
        question2
    };
}
