using UnityEngine;
using LevelData;
using System.Collections.Generic;
using System;

public class JsonToLevelData
{
    private Data data;
    private List<string> question = new List<string>();
    private List<string> answer = new List<string>();
    private List<string> timeofday = new List<string>();
    private List<string> timeofyear = new List<string>();

    public JsonToLevelData(String json)
    {
        data = JsonUtility.FromJson<Data>(json);
        foreach (var item in data.Datas)
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