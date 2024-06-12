using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public Texture2D TestPuzzle;
    public RawImage RawImage;

    [SerializeField]
    private Vector2 basicSize;

    private int[] puzzle;
    private int maxgridID = 99999;

    private float newHeight;
    private float newWidth;

    [SerializeField]
    private float intervelSize = 170f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [ContextMenu("LoadTexture")]
    public void LoadTextureTest()
    {
        LoadTexture(TestPuzzle);
    }

    public void LoadTexture(Texture2D texture2D)
    {
        RawImage.texture = texture2D;
        TextureScaleResize(texture2D);
        RawImage.color = new Color(1, 1, 1, 0.7f);
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
        RawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth * intervelSize, newHeight * intervelSize);
        InitPuzzleGrid();
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

    private void InitPuzzleGrid()
    {
        puzzle = null;
        puzzle = new int[(int)(newWidth * newHeight)];
        for (int i = 0; i < puzzle.Length; i++)
        {
            puzzle[i] = maxgridID;
        }
    }

    public bool IstherePiece(int gridID)
    {
        if (puzzle.Length <= gridID)
        {
            return false;
        }
        if (puzzle[gridID] != maxgridID)
        {
            return true;
        }
        return false;
    }

    public void SetPiece(int gridID, int pieceID)
    {
        puzzle[gridID] = pieceID;
    }

    public void OutPiece(int gridID)
    {
        puzzle[gridID] = maxgridID;
    }

    public bool IsFinish()
    {
        for (int i = 0; i < puzzle.Length; i++)
        {
            if (puzzle[i] >= i + 1)
            {
                return false;
            }
        }
        return true;
    }

}
