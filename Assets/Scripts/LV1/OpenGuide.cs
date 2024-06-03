using UnityEngine;

public class OpenGuide : MonoBehaviour
{
    public Canvas canvas;  // Ҫ�򿪵�Canvas����

    // �������ڴ�Canvas
    void Start()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false); // ��ʼ״̬�����ػ���
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(!canvas.gameObject.activeSelf); // �л���������ʾ״̬
            }
        }
    }
}