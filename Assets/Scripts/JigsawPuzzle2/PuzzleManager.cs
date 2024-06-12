using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleManager2 : MonoBehaviour
{
    public static PuzzleManager2 Instance;
    bool gameStart;
    float timer;
    public GameObject StartPanel;
    public GameObject EndPanel;
    public Drawer drawer;
    /// <summary>
    /// 新的图片分辨率
    /// </summary>
    [HideInInspector]
    public float newW, newH;
    /// <summary>
    /// 碎片间隔
    /// </summary>
    public float intervalSize = 170;
    /// <summary>
    /// 图片
    /// </summary>
    [SerializeField]
    RawImage rawImage;

    /// <summary>
    /// 限定图片大小范围
    /// </summary>
    [SerializeField]
    Vector2 basicSize;
    [SerializeField]
    Text timeText;
    [SerializeField]
    Text EndTimeText;
    int[] puzzle;//碎片格子
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (gameStart)
        {
            timer += Time.deltaTime;
            timeText.text = ((int)timer).ToString();
        }
    }

    /// <summary>
    /// 读取图片
    /// </summary>
    [UnityEngine.ContextMenu("LoadTexture")]
    public void LoadTexture()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JPG|*.jpg|PNG图片|*.png";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            ///获取所选择的图片
            string texPath = openFileDialog.FileName;
            Texture2D tex2d = new Texture2D(1, 1);
            byte[] texByte = File.ReadAllBytes(texPath);
            tex2d.LoadImage(texByte);

            //重新计算图片大小
            rawImage.texture = tex2d;
            TextureScaleResize(tex2d);
            rawImage.color = new Color(1, 1, 1, 0.5f);
            newW = (int)(newW / intervalSize);
            newH = (int)(newH / intervalSize);
            if (newW % 2 != 0)
            {
                newW += 1;
            }
            if (newH % 2 != 0)
            {
                newH += 1;
            }
            //自动生成碎片
            PuzzlePool2.Instance.CreatePuzzle((int)newH, (int)newW, tex2d, intervalSize);
            rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(newW * intervalSize, newH * intervalSize);
            InitPuzzleGrid();
            StartPanel.SetActive(false);
            drawer.InitScript();
            timer = 0;
            gameStart = true;
        }
    }

    /// <summary>
    /// 重新计算图片大小，同比例缩放
    /// </summary>
    /// <param name="tex">图片</param>
    void TextureScaleResize(Texture2D tex)
    {
        bool isWidth = tex.width > tex.height ? true : false;
        float longSide = isWidth == true ? tex.width : tex.height;

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

        newW = expand == true ? tex.width / ratio : tex.width * ratio;
        newH = expand == true ? tex.height / ratio : tex.height * ratio;

        //解决伪正方形的问题
        if (newH > basicSize.y)
        {
            ratio = basicSize.y / newH;
            newW = newW * ratio;
            newH = newH * ratio;
        }
        newW = (int)newW;
        newH = (int)newH;
    }

    /// <summary>
    /// 初始化格子
    /// </summary>
    void InitPuzzleGrid()
    {
        puzzle = null;
        puzzle = new int[(int)(newW * newH)];
        for (int i = 0; i < puzzle.Length; i++)
        {
            puzzle[i] = 99999;
        }
    }

    /// <summary>
    /// 检测该格子下是否有碎片
    /// </summary>
    /// <param name="gridID"></param>
    public bool CheckHavePiece(int gridID)
    {
        if (puzzle.Length <= gridID)
        {
            Debug.LogError("检查格子碎片时超出索引");
            return false;
        }
        if (puzzle[gridID] != 99999)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 网格填进碎片
    /// </summary>
    /// <param name="gridID">网格ID</param>
    /// <param name="pieceID">碎片ID</param>
    public void SetPiece(int gridID, int pieceID)
    {
        puzzle[gridID] = pieceID;
    }

    /// <summary>
    /// 取出碎片
    /// </summary>
    /// <param name="gridID">网格ID</param>
    public void OutPiece(int gridID)
    {
        puzzle[gridID] = 99999;
    }

    /// <summary>
    /// 检测是否完成拼图
    /// </summary>
    /// <returns></returns>
    public bool IsFinish()
    {
        for (int i = 0; i < puzzle.Length - 1; i++)
        {
            if (puzzle[i] >= puzzle[i + 1])
            {
                return false;
            }
        }
        Debug.Log("拼图完成");
        EndPanel.SetActive(true);
        PuzzlePool2.Instance.Recycle();
        EndTimeText.text = "用时:" + timeText.text + "秒";
        gameStart = false;
        timer = 0;
        return true;
    }

}
