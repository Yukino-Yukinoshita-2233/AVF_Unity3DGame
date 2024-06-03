using UnityEngine;

public class OpenGuide : MonoBehaviour
{
    public Canvas canvas;  // 要打开的Canvas对象

    // 方法用于打开Canvas
    void Start()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false); // 初始状态下隐藏画布
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(!canvas.gameObject.activeSelf); // 切换画布的显示状态
            }
        }
    }
}