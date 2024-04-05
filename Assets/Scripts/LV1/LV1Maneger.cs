using LevelData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LV1Maneger : MonoBehaviour
{
    private JsonToLevelData jsonToLevelData;
    [SerializeField] Text question;
    [SerializeField] Text answer;
    [SerializeField] Button lookAnswerButtom;
    [SerializeField] Button nextQuestionButtom;
    bool islookButton = false;
    int rangeSum = 0;
    int questionsCount = 0;
    private void Start()
    {
        //question = gameObject.GetComponent<Text>();
        var json = Resources.Load<TextAsset>("Data/Level1").text;
        if (json == null) { Debug.Log("Can't get Level1"); }
        // 创建JsonToLevelData实例并传递JSON数据
        jsonToLevelData = new JsonToLevelData(json);
        SelectQuestion(jsonToLevelData);
        lookAnswerButtom.onClick.AddListener(GetlookAnswerButtomdown);
        nextQuestionButtom.onClick.AddListener(GetnextQuestionbuttom);

    }

    void SelectQuestion(JsonToLevelData jsonToLevelData)
    {        
        // 获取问题和答案列表
        var questions = jsonToLevelData.GetQuestion();
        var answers = jsonToLevelData.GetAnswer();
        var TimeofDay = jsonToLevelData.GetTimeofDay();
        var TimeofYear = jsonToLevelData.GetTimeofYear();
        questionsCount = questions.Count;
        // 输出数据到场景
        question.text = questions[rangeSum];
        if (islookButton == true) { answer.text = answers[rangeSum]; }
        else { answer.text = null; }
        Sun.TimeofDay = float.Parse(TimeofDay[rangeSum]);
        Sun.TimeofYear = float.Parse(TimeofYear[rangeSum]);
    }

    void GetlookAnswerButtomdown()
    {
        islookButton = (islookButton == false) ? true : false;
        Debug.Log("lookAnswer: " + islookButton);

        SelectQuestion(jsonToLevelData);

    }
    void GetnextQuestionbuttom()
    {
        Debug.Log("nextQuestion");

        islookButton = false;
        rangeSum = Random.Range(0, questionsCount);
        SelectQuestion(jsonToLevelData);
    }
}