using LevelData;
using Newtonsoft.Json;
using System.Collections.Generic;

public class JsonToLevelData
{
    private List<Data> data = new List<Data>();
    private List<string> question = new List<string>();
    private List<string> answer = new List<string>();
    private List<string> answerA = new List<string>();
    private List<string> answerB = new List<string>();
    private List<string> answerC = new List<string>();
    private List<string> answerD = new List<string>();
    private List<string> timeofday = new List<string>();
    private List<string> timeofyear = new List<string>();

    public JsonToLevelData(string json)
    {
        data = JsonConvert.DeserializeObject<List<Data>>(json);
        foreach (var item in data)
        {
            question.Add(item.Question);
            answer.Add(item.Answer);
            answerA.Add(item.AnswerA);
            answerB.Add(item.AnswerB);
            answerC.Add(item.AnswerC);
            answerD.Add(item.AnswerD);
            timeofday.Add(item.TimeofDay);
            timeofyear.Add(item.TimeofYear);
        }
    }

    public List<string> GetQuestion()
    {

        return question;
    }

    public List<string> GetAnswer()
    {
        return answer;
    }
    public List<string> GetAnswerA()
    {
        return answerA;
    }
    public List<string> GetAnswerB()
    {
        return answerB;
    }
    public List<string> GetAnswerC()
    {
        return answerC;
    }
    public List<string> GetAnswerD()
    {
        return answerD;
    }

    public List<string> GetTimeofDay()
    {
        return timeofday;
    }

    public List<string> GetTimeofYear()
    {
        return timeofyear;
    }

}