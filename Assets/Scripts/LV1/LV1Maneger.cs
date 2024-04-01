using LevelData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LV1Maneger : MonoBehaviour
{
    private JsonToLevelData jsonToLevelData;
    [SerializeField] Text question = null;
    [SerializeField] Text answer = null;



    private void Start()
    {
        var json = Resources.Load<TextAsset>("Data/Level1").text;
        if (json != null)
        {
            Debug.Log("Can't get Level1");
        }
        // 创建JsonToLevelData实例并传递JSON数据
        jsonToLevelData = new JsonToLevelData(json);

        // 获取问题和答案列表
        var questions = jsonToLevelData.GetQuestion();
        var answers = jsonToLevelData.GetAnswer();
        var TimeofDay = jsonToLevelData.GetTimeofDay();
        var TimeofYear = jsonToLevelData.GetTimeofYear();
        SelectQuestion(questions, answers,TimeofDay,TimeofYear);

    }

    void SelectQuestion(List<string> questions, List<string> answers,List<string> timeofDay, List<string> timeofYear)
    {
        int rangeSum = Random.Range(0, questions.Count);
        question.text = questions[rangeSum];
        answer.text = answers[rangeSum];
        Sun.TimeofDay = float.Parse(timeofDay[rangeSum]);
        Sun.TimeofYear = float.Parse(timeofYear[rangeSum]);
        Debug.Log("Sun.TimeofDay: " + Sun.TimeofDay);
        Debug.Log("Sun.TimeofYear: " + Sun.TimeofYear);
    }
}