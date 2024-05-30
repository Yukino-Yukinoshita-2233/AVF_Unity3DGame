using LevelData;
using UnityEngine;
using UnityEngine.UI;

public class LV1Manager : MonoBehaviour
{
    private JsonToLevelData jsonToLevelData;
    [SerializeField] private Text question;
    [SerializeField] private Text answer;
    [SerializeField] private Text answerA;
    [SerializeField] private Text answerB;
    [SerializeField] private Text answerC;
    [SerializeField] private Text answerD;
    [SerializeField] private Text AnswerTF;
    [SerializeField] private Button AnswerAButton;
    [SerializeField] private Button AnswerBButton;
    [SerializeField] private Button AnswerCButton;
    [SerializeField] private Button AnswerDButton;
    [SerializeField] private Button lookAnswerButton;
    [SerializeField] private Button nextQuestionButton;

    private bool isLookButton = false;
    private bool isAnswerTrue = false;
    private int rangeSum = 0;
    private int questionsCount = 0;
    private Text playerAnswer;

    private void Start()
    {
        playerAnswer = new GameObject("PlayerAnswer").AddComponent<Text>();
        playerAnswer.gameObject.SetActive(false);

        var json = Resources.Load<TextAsset>("Data/Level1")?.text;
        if (json == null)
        {
            Debug.LogError("Can't get Level1");
            return;
        }

        jsonToLevelData = new JsonToLevelData(json);
        questionsCount = jsonToLevelData.GetQuestion().Count;
        SetupButtons();
        Cursor.lockState = CursorLockMode.Locked;
        SelectQuestion();
    }

    private void SetupButtons()
    {
        AnswerAButton.onClick.AddListener(() => SetPlayerAnswer(answerA));
        AnswerBButton.onClick.AddListener(() => SetPlayerAnswer(answerB));
        AnswerCButton.onClick.AddListener(() => SetPlayerAnswer(answerC));
        AnswerDButton.onClick.AddListener(() => SetPlayerAnswer(answerD));
        lookAnswerButton.onClick.AddListener(ToggleAnswerVisibility);
        nextQuestionButton.onClick.AddListener(SelectNextQuestion);
    }

    private void SelectQuestion()
    {
        var questions = jsonToLevelData.GetQuestion();
        var answers = jsonToLevelData.GetAnswer();
        var answersAs = jsonToLevelData.GetAnswerA();
        var answersBs = jsonToLevelData.GetAnswerB();
        var answersCs = jsonToLevelData.GetAnswerC();
        var answersDs = jsonToLevelData.GetAnswerD();
        var TimeofDay = jsonToLevelData.GetTimeofDay();
        var TimeofYear = jsonToLevelData.GetTimeofYear();

        question.text = questions[rangeSum];
        answer.text = answers[rangeSum];
        answerA.text = answersAs[rangeSum];
        answerB.text = answersBs[rangeSum];
        answerC.text = answersCs[rangeSum];
        answerD.text = answersDs[rangeSum];

        Sun.TimeOfDay = float.Parse(TimeofDay[rangeSum]);
        Sun.TimeOfYear = float.Parse(TimeofYear[rangeSum]);
        Debug.Log("TimeOfDay"+Sun.TimeOfDay);
        Debug.Log("TimeOfYear"+Sun.TimeOfYear);
        answer.gameObject.SetActive(false);
        AnswerTF.text = string.Empty;
    }

    private void ToggleAnswerVisibility()
    {
        isLookButton = !isLookButton;
        answer.gameObject.SetActive(isLookButton);

        if (isLookButton)
        {
            AnswerTF.text = isAnswerTrue ? "答案正确！" : "答案错误，请重试。";
        }
        else
        {
            AnswerTF.text = string.Empty;
        }
    }

    private void SelectNextQuestion()
    {
        isLookButton = false;
        answer.gameObject.SetActive(false);

        int newRange;
        do
        {
            newRange = Random.Range(0, questionsCount);
        } while (newRange == rangeSum);

        rangeSum = newRange;
        SelectQuestion();
    }

    private void SetPlayerAnswer(Text selectedAnswer)
    {
        playerAnswer.text = selectedAnswer.text;
        isAnswerTrue = playerAnswer.text == answer.text;
    }
}
