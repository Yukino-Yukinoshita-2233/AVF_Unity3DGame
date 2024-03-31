using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        // 获取Button组件
        Button button = GetComponent<Button>();

        // 添加按钮点击事件
        button.onClick.AddListener(QuitGame);
    }

    void QuitGame()
    {
        // 在编辑器中运行时，退出游戏
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在构建的应用中，退出游戏
        Application.Quit();
#endif
    }
}