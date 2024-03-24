using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        // ��ȡButton���
        Button button = GetComponent<Button>();

        // ��Ӱ�ť����¼�
        button.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        // �ڱ༭��������ʱ���˳���Ϸ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �ڹ�����Ӧ���У��˳���Ϸ
        Application.Quit();
#endif
    }
}