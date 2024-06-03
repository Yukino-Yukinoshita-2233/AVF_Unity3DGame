using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextimage : MonoBehaviour
{
    public Image[] images; // Ҫ�л���ͼƬ����
    private int currentIndex = 0; // ��ǰ��ʾ��ͼƬ����

    void Start()
    {
        UpdateImages();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchToNextImage();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToLastImage();
        }
    }

    public void SwitchToNextImage()
    {
        currentIndex = (currentIndex + 1) % images.Length; // �л�����һ��ͼƬ����
        UpdateImages();
    }
    public void SwitchToLastImage()
    {
        currentIndex = (currentIndex - 1) % images.Length; // �л�����һ��ͼƬ����
        UpdateImages();
    }

    private void UpdateImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex); // ����ʾ��ǰ������Ӧ��ͼƬ
        }
    }
}
