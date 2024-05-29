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
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;
    [SerializeField] Text AnswerTF;
    [SerializeField] Button lookAnswerButtom;
    [SerializeField] Button nextQuestionButtom;
    bool islookButton = false;
    bool isAnswerTrue = false;
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
        Cursor.lockState = CursorLockMode.Locked;
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
        answer.text = (islookButton == true) ? answers[rangeSum] : null;
        isAnswerTrue = (resultText.text == answers[rangeSum]);

        Sun.TimeofDay = float.Parse(TimeofDay[rangeSum]);
        Sun.TimeofYear = float.Parse(TimeofYear[rangeSum]);
    }

    void GetlookAnswerButtomdown()
    {
        islookButton = (islookButton == false);

        SelectQuestion(jsonToLevelData);
        AnswerTF.text = (islookButton == true) ? (AnswerTF.text = (isAnswerTrue == true) ? "答案正确！" : "答案错误，请重试。") : null;


    }
    void GetnextQuestionbuttom()
    {
        Debug.Log("nextQuestion");
        islookButton = false;
        int rangex = Random.Range(0, questionsCount);
        while(rangeSum == rangex) { rangex = Random.Range(0, questionsCount); }
        rangeSum = rangex;
        resultText.text = null;
        AnswerTF.text = null;

        SelectQuestion(jsonToLevelData);
    }
}