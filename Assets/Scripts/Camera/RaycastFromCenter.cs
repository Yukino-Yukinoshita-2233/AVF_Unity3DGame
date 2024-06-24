using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastFromCenter : MonoBehaviour
{
    public Camera mainCamera;
    public EventSystem eventSystem;
    public GraphicRaycaster raycaster;

    private void Start()
    {
        ToHideCursor();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // 当鼠标左键被按下时
        {
            // 从屏幕中心位置创建射线
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = screenCenterPoint;

            // 使用 GraphicRaycaster 检测 UI 碰撞
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            // 检查是否有碰撞到 UI 元素
            foreach (RaycastResult result in results)
            {
                // 如果碰撞到的是一个按钮
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    // 触发按钮的点击事件
                    button.onClick.Invoke();
                    Debug.Log("Button clicked: " + button.name);
                }
            }
        }
    }

    void ToHideCursor()
    {
        Cursor.visible = false;
    }


}
