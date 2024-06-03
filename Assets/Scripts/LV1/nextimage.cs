using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextimage : MonoBehaviour
{
    public Image[] images; // 要切换的图片数组
    private int currentIndex = 0; // 当前显示的图片索引

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
        currentIndex = (currentIndex + 1) % images.Length; // 切换到下一个图片索引
        UpdateImages();
    }
    public void SwitchToLastImage()
    {
        currentIndex = (currentIndex - 1) % images.Length; // 切换到上一个图片索引
        UpdateImages();
    }

    private void UpdateImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex); // 仅显示当前索引对应的图片
        }
    }
}
