﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PieceType2
{
    Grid, TL, TR, T1, T2, BL, BR, B1, B2, L1, L2, R1, R2, C1, C2
}
public class PuzzlePool2 : MonoBehaviour
{
    public static PuzzlePool2 Instance;
    public float maxX, minX, maxY, minY;
    [SerializeField]
    GameObject grid, TL, TR, T1, T2, BL, BR, B1, B2, L1, L2, R1, R2, C1, C2;
    [SerializeField]
    Transform GridPanel, BornPanel;
    List<GameObject> gridList = new List<GameObject>();
    //对象池
    Dictionary<string, List<GameObject>> PieceDic = new Dictionary<string, List<GameObject>>();
    //用于回收
    List<GameObject> recyclePiece = new List<GameObject>();
    [SerializeField]
    Vector2 textureStartPos, offsetX, offsetY;
    Vector2 tempV2, size;
    Texture2D tex;
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 创建碎片
    /// </summary>
    /// <param name="row">高</param>
    /// <param name="col">宽</param>
    /// <param name="t">图片</param>
    /// <param name="interval">间隔</param>
    public void CreatePuzzle(int row, int col, Texture2D t, float interval)
    {
        tex = t;
        tempV2 = textureStartPos;
        int totalGridNum = row * col;
        size = new Vector2(col * interval, row * interval);
        GameObject g = null;
        //生成二维格子
        for (int i = 0; i < totalGridNum; i++)
        {
            g = GetPiece(PieceType.Grid, i);
            g.transform.SetParent(GridPanel);
            gridList.Add(g);
            recyclePiece.Add(g);
        }
        StartCoroutine(IECreate(row, col));
    }

    /// <summary>
    /// 真正根据宽高生成碎片
    /// </summary>
    /// <param name="row">高</param>
    /// <param name="col">宽</param>
    /// <returns></returns>
    IEnumerator IECreate(int row, int col)
    {
        yield return new WaitForSeconds(0.5f);
        CreatePieces(row, col);
    }


    void CreatePieces(int row, int col)
    {
        int index = 0;
        GameObject picese = null;
        RectTransform temp;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (CheckCorner(i, j, row, col, ref picese, index))
                {
                    //求角
                }
                else if (CheckSide(i, j, row, col, ref picese, index))
                {
                    //求边
                }
                else
                {
                    //中间碎片
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        picese = GetPiece(PieceType.C1, index);

                    }
                    else
                    {
                        picese = GetPiece(PieceType.C2, index);
                    }
                }
                picese.transform.SetParent(BornPanel);
                //将二位格子的位置给到碎片
                picese.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                //每一个碎片附上图片
                picese.transform.Find("Mask/texture").GetComponent<RawImage>().texture = tex;
                //重新给图片设置大小
                temp = picese.transform.Find("Mask/texture").GetComponent<RectTransform>();
                temp.sizeDelta = size;
                //做图片的偏移
                temp.anchoredPosition = tempV2;
                tempV2 += offsetX;
                index++;
                recyclePiece.Add(picese);
            }
            //换行做图片的偏移
            tempV2.x = textureStartPos.x;
            tempV2 += offsetY;
        }
    }


    /// <summary>
    /// 检测是否为边
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="row">高</param>
    /// <param name="col">宽</param>
    /// <param name="obj">物体</param>
    /// <returns></returns>
    bool CheckSide(int i, int j, int row, int col, ref GameObject obj, int index)
    {
        //上边
        if (i == 0 && j != 0 && j != col - 1)
        {
            if (j % 2 != 0)
            {
                obj = GetPiece(PieceType.T1, index);

            }
            else
            {
                obj = GetPiece(PieceType.T2, index);

            }
            return true;
        }
        //下边
        else if (i == row - 1 && j != 0 && j != col - 1)
        {
            if (j % 2 != 0)
            {
                obj = GetPiece(PieceType.B1, index);

            }
            else
            {
                obj = GetPiece(PieceType.B2, index);

            }
            return true;
        }
        else if (j == 0 && i != 0 && j != col - 1)
        {
            if (i % 2 != 0)
            {
                obj = GetPiece(PieceType.L1, index);
            }
            else
            {
                obj = GetPiece(PieceType.L2, index);

            }
            return true;
        }
        else if (j == col - 1 && i != 0 && i != row - 1)
        {
            if (i % 2 != 0)
            {
                obj = GetPiece(PieceType.R1, index);

            }
            else
            {
                obj = GetPiece(PieceType.R2, index);

            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// 检测是否为角
    /// </summary>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    bool CheckCorner(int i, int j, int row, int col, ref GameObject obj, int index)
    {
        //TL
        if (i == 0 && j == 0)
        {
            obj = GetPiece(PieceType.TL, index);

            return true;
        }
        //TR
        else if (i == 0 && j == col - 1)
        {
            obj = GetPiece(PieceType.TR, index);

            return true;
        }
        //BL
        else if (j == 0 && i == row - 1)
        {
            obj = GetPiece(PieceType.BL, index);

            return true;
        }
        //BR
        else if (j == col - 1 && i == row - 1)
        {
            obj = GetPiece(PieceType.BR, index);

            return true;
        }
        return false;
    }

    /// <summary>
    /// 根据类型获取碎片
    /// </summary>
    /// <param name="type">碎片类型</param>
    /// <param name="id">设置名称</param>
    /// <returns></returns>
    GameObject GetPiece(PieceType type, int id)
    {
        /*
         * 1、获取类型转成string
         * 2、检测是否有该类的碎片List
         * 3、检测该List 是否有可用的碎片
         * 4、创建碎片
         * 5、加入回收
         */
        string str = type.ToString();
        GameObject o = null;
        if (PieceDic.ContainsKey(str))
        {
            bool find = false;
            foreach (var i in PieceDic[str])
            {
                if (!i.activeSelf)
                {
                    o = i;
                    find = true;
                    break;
                }
            }
            if (!find)
            {
                o = CreatePiece(type);
                PieceDic[str].Add(o);
                //创建一个碎片，并加入list中
            }
        }
        else
        {
            List<GameObject> list = new List<GameObject>();
            //创建一个碎片，加入list中
            o = CreatePiece(type);
            list.Add(o);
            PieceDic.Add(str, list);
        }
        o.name = id.ToString();
        o.SetActive(true);
        return o;
    }

    /// <summary>
    /// 根据类型创建预制体
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns></returns>
    GameObject CreatePiece(PieceType type)
    {
        switch (type)
        {
            case PieceType.Grid:
                return Instantiate(grid);

            case PieceType.C1:
                return Instantiate(C1);

            case PieceType.C2:
                return Instantiate(C2);

            case PieceType.T1:
                return Instantiate(T1);

            case PieceType.T2:
                return Instantiate(T2);

            case PieceType.B1:
                return Instantiate(B1);

            case PieceType.B2:
                return Instantiate(B2);

            case PieceType.R2:
                return Instantiate(R2);

            case PieceType.R1:
                return Instantiate(R1);
            case PieceType.L1:
                return Instantiate(L1);

            case PieceType.L2:
                return Instantiate(L2);

            case PieceType.TL:
                return Instantiate(TL);

            case PieceType.TR:
                return Instantiate(TR);

            case PieceType.BL:
                return Instantiate(BL);

            case PieceType.BR:
                return Instantiate(BR);
            default:
                return null;
        }
    }

    /// <summary>
    /// 回收预制体
    /// </summary>
    [ContextMenu("Recycle")]
    public void Recycle()
    {
        foreach (var i in recyclePiece)
        {
            i.SetActive(false);
        }
        gridList.Clear();
        recyclePiece.Clear();
    }
}
