using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PieceType
{
    Grid, TL, T1, T2, TR, L1, C1, C2, R1, L2, R2, BL, B1, B2, BR
}

public class PuzzlePool : MonoBehaviour
{
    public static PuzzlePool Instance;

    [SerializeField]
    private GameObject grid, TL, T1, T2, TR, L1, C1, C2, R1, L2, R2, BL, B1, B2, BR;
    [SerializeField]
    private Transform GridPanel;
    [SerializeField]
    private Transform PiecePanel;
    private List<GameObject> gridList = new List<GameObject>();

    private Dictionary<string, List<GameObject>> pieceDic = new Dictionary<string, List<GameObject>>();
    private List<GameObject> recyclePiece = new List<GameObject>();

    [SerializeField]
    private Vector2 textureStartPos = new Vector2(65, -65);
    [SerializeField]
    private Vector2 offsetX = new Vector2(170, 0);
    [SerializeField]
    private Vector2 offsetY = new Vector2(0, -170);
    private Vector2 tempV2;
    private Vector2 intervalSize;
    private Texture2D texture2D;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreatePuzzle(int row, int col, Texture2D texture, float interval)
    {
        texture2D = texture;
        tempV2 = textureStartPos;
        int totalGridNum = row * col;
        intervalSize = new Vector2(col * interval, row * interval);
        GameObject obj;
        for (int i = 0; i < totalGridNum; i++)
        {
            // gridList.Add(Instantiate(grid, GridPanel));
            obj = GetPiece(PieceType.Grid, i);
            obj.transform.SetParent(GridPanel);
            gridList.Add(obj);
            recyclePiece.Add(obj);
        }

        StartCoroutine(ICreatePieces(row, col));

    }

    IEnumerator ICreatePieces(int row, int col)
    {
        yield return new WaitForSeconds(0.5f);
        CreatePieces(row, col);
    }

    private void CreatePieces(int row, int col)
    {
        int index = 0;
        GameObject piece = null;
        RectTransform temp;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (CheckCorner(i, j, row, col, ref piece, index))
                {
                    // 求角
                }
                else if (CheckSide(i, j, row, col, ref piece, index))
                {
                    // 求边
                }
                else
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        // piece = Instantiate(C1);
                        piece = GetPiece(PieceType.C1, index);
                    }
                    else
                    {
                        // piece = Instantiate(C2);
                        piece = GetPiece(PieceType.C2, index);
                    }
                }
                piece.transform.SetParent(PiecePanel);

                piece.GetComponent<RectTransform>().anchoredPosition = gridList[index].transform.position;
                piece.transform.Find("Mask/Texture").GetComponent<RawImage>().texture = texture2D;

                temp = piece.transform.Find("Mask/Texture").GetComponent<RectTransform>();
                temp.sizeDelta = intervalSize;

                temp.anchoredPosition = tempV2;
                tempV2 += offsetX;

                index++;

                recyclePiece.Add(piece);
            }
            tempV2.x = textureStartPos.x;
            tempV2 += offsetY;

            Debug.Log(tempV2);
        }
    }

    private bool CheckSide(int i, int j, int row, int col, ref GameObject obj, int index)
    {
        // Top Side
        if (i == 0 && j != 0 && j != col - 1)
        {
            if (j % 2 != 0)
            {
                // obj = Instantiate(T1);
                obj = GetPiece(PieceType.T1, index);
            }

            else
            {
                // obj = Instantiate(T2);
                obj = GetPiece(PieceType.T2, index);
            }

            return true;
        }

        // Bottom Side
        else if (i == row - 1 && j != 0 && j != col - 1)
        {
            if (j % 2 != 0)
            {
                // obj = Instantiate(B1);
                obj = GetPiece(PieceType.B1, index);
            }
            else
            {
                // obj = Instantiate(B2);
                obj = GetPiece(PieceType.B2, index);
            }
            return true;
        }

        // Left Side
        else if (j == 0 && i != 0 && j != col - 1)
        {
            if (i % 2 != 0)
            {
                // obj = Instantiate(L1);
                obj = GetPiece(PieceType.L1, index);
            }
            else
            {
                // obj = Instantiate(L2);
                obj = GetPiece(PieceType.L2, index);
            }
            return true;
        }

        // Right Side
        else if (j == col - 1 && i != 0 && i != row - 1)
        {
            if (i % 2 != 0)
            {
                // obj = Instantiate(R1);
                obj = GetPiece(PieceType.R1, index);
            }
            else
            {
                // obj = Instantiate(R2);
                obj = GetPiece(PieceType.R2, index);
            }
            return true;
        }


        return false;
    }

    private bool CheckCorner(int i, int j, int row, int col, ref GameObject obj, int index)
    {
        // Top Left Side
        if (i == 0 && j == 0)
        {
            // obj = Instantiate(TL);
            obj = GetPiece(PieceType.TL, index);
            return true;
        }

        // Top Right Side
        else if (i == 0 && j == col - 1)
        {
            // obj = Instantiate(TR);
            obj = GetPiece(PieceType.TR, index);
            return true;
        }

        // Bottom Left Side
        else if (j == 0 && i == row - 1)
        {
            // obj = Instantiate(BL);
            obj = GetPiece(PieceType.BL, index);
            return true;
        }

        // Bottom Right Side
        else if (j == col - 1 && i == row - 1)
        {
            // obj = Instantiate(BR);
            obj = GetPiece(PieceType.BR, index);
            return true;
        }

        return false;
    }

    private GameObject GetPiece(PieceType pieceType, int id)
    {
        string str = pieceType.ToString();
        GameObject gobj = null;
        if (pieceDic.ContainsKey(str))
        {
            bool isFind = false;
            foreach (var item in pieceDic[str])
            {
                if (!item.activeSelf)
                {
                    isFind = true;
                    gobj = item;
                    break;
                }
            }

            if (!isFind)
            {
                gobj = CreatePiece(pieceType);
                pieceDic[str].Add(gobj);
            }
        }
        else
        {
            List<GameObject> list = new List<GameObject>();
            gobj = CreatePiece(pieceType);
            list.Add(gobj);
            pieceDic.Add(str, list);
        }

        gobj.name = id.ToString();
        gobj.SetActive(true);
        return gobj;
    }

    private GameObject CreatePiece(PieceType pieceType)
    {
        switch (pieceType)
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
            case PieceType.R1:
                return Instantiate(R1);
            case PieceType.R2:
                return Instantiate(R2);
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

    [ContextMenu("RecyclePiece")]
    public void RecyclePice()
    {
        foreach (var p in recyclePiece)
        {
            p.SetActive(false);
        }

        gridList.Clear();
        recyclePiece.Clear();
    }
}
