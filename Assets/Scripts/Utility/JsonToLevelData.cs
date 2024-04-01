using LevelData;
using Newtonsoft.Json;
using System.Collections.Generic;

public class JsonToLevelData
{
    private List<Data> data = new List<Data>();
    private List<string> question = new List<string>();
    private List<string> answer = new List<string>();
    private List<string> timeofday = new List<string>();
    private List<string> timeofyear = new List<string>();

    public JsonToLevelData(string json)
    {
        data = JsonConvert.DeserializeObject<List<Data>>(json);
        foreach (var item in data)
        {
            question.Add(item.Question);
            answer.Add(item.Answer);
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

    public List<string> GetTimeofDay()
    {
        return timeofday;
    }

    public List<string> GetTimeofYear()
    {
        return timeofyear;
    }

}