using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine.Video;

public class Question
{
    private int scoreValue;

    private string type;
    private string questionText;
    private int questionAnswersCount;

    public void SetScoreValue(int _scoreValue)
    {
        scoreValue = _scoreValue;
    }

    ///// <summary>
    ///// Sets the Question type: 0 - Text, 1 - Image, 2 - Video
    ///// </summary>
    ///// <param name="typeNumber"></param>
    //public void SetType(int typeNumber)
    //{
    //    switch (typeNumber)
    //    {
    //        case 0:
    //            type = "Text";
    //            break;
    //        case 1:
    //            type = "Image";
    //            break;
    //        case 2:
    //            type = "Video";
    //            break;
    //        default:
    //            break;
    //    }
    //}
}

public class QuestionText : Question
{
    private Image imageText;
}

public class QuestionPhoto : Question
{
    private Image photo;
}

public class QuestionVideo : Question
{
    private VideoClip video;
}
