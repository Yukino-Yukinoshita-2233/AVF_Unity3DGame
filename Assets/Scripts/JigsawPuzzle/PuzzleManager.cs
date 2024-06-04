using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private Vector2 basicSize;

    private float newHeight;
    private float newWidth;

    private float intervelSize = 170f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void LoadTexture(string path)
    {
        Texture2D texture2D = new Texture2D(1, 1);
        byte[] fileData = File.ReadAllBytes(path);
        texture2D.LoadImage(fileData);
        rawImage.texture = texture2D;
        TextureScaleResize(texture2D);

        newHeight = (int)(newHeight / intervelSize);
        newWidth = (int)(newWidth / intervelSize);

        if (newWidth % 2 != 0)
        {
            newWidth += 1;
        }
        if (newHeight % 2 != 0)
        {
            newHeight += 1;
        }

        PuzzlePool.Instance.CreatePuzzle((int)newHeight, (int)newWidth, texture2D, intervelSize);
        rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth * intervelSize, newHeight * intervelSize);

    }

    void TextureScaleResize(Texture2D texture2D)
    {
        bool isWidth = texture2D.width > texture2D.height ? true : false;
        float longSide = isWidth == true ? texture2D.width : texture2D.height;

        bool expand;
        float ratio;

        if (isWidth)
        {
            expand = longSide > basicSize.x ? false : true;
            ratio = expand == true ? longSide / basicSize.x : basicSize.x / longSide;
        }
        else
        {
            expand = longSide > basicSize.y ? false : true;
            ratio = expand == true ? longSide / basicSize.y : basicSize.y / longSide;
        }

        newWidth = expand == true ? texture2D.width / ratio : texture2D.width * ratio;
        newHeight = expand == true ? texture2D.height / ratio : texture2D.height * ratio;
    }
}
