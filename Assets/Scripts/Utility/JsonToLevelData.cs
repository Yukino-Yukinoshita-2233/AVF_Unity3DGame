using UnityEngine;
using LevelData;
using System;

public class JsonToLevelData
{
    private Data data;

    public JsonToLevelData(String json)
    {
        data = JsonUtility.FromJson<Data>(json);
    }

    public string GetQuestion()
    {
        return data.Datas[0].Question;
    }

    public string GetAnswer()
    {
        return data.Datas[0].Answer;
    }

    public string GetTimeofDay()
    {
        return data.Datas[0].TimeofDay;
    }

    public string GetTimeofYear()
    {
        return data.Datas[0].TimeofYear;
    }



}