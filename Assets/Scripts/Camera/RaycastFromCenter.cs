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

        if (Input.GetMouseButtonDown(0)) // ��������������ʱ
        {
            // ����Ļ����λ�ô�������
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = screenCenterPoint;

            // ʹ�� GraphicRaycaster ��� UI ��ײ
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            // ����Ƿ�����ײ�� UI Ԫ��
            foreach (RaycastResult result in results)
            {
                // �����ײ������һ����ť
                Button button = result.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    // ������ť�ĵ���¼�
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
