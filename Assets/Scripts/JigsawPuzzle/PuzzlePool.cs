using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PuzzlePool : MonoBehaviour
{
    public static PuzzlePool Instance;

    [SerializeField]
    private GameObject grid, TL, T1, T2, TR, L1, C1, C2, R1, L2, R2, BL, B1, B2, BR;
    [SerializeField]
    private Transform puzzlePanel;
    [SerializeField]
    private Transform canvasTransform;
    private List<GameObject> gridList = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreatePuzzle(int row, int col)
    {
        int totalGridNum = row * col;
        for (int i = 0; i < totalGridNum; i++)
        {
            gridList.Add(Instantiate(grid, puzzlePanel));
        }

        StartCoroutine(ICreatePieces(row, col));

    }

    IEnumerator ICreatePieces(int row, int col)
    {
        yield return new WaitForSeconds(1);
        CreatePieces(row, col);
    }

    private void CreatePieces(int row, int col)
    {
        int index = 0;
        GameObject piece = null;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                if (CheckCorner(i, j, row, col, ref piece))
                {

                }
                else if (CheckSide(i, j, row, col, ref piece))
                {

                }
                else
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        piece = Instantiate(C1);
                    }
                    else
                    {
                        piece = Instantiate(C2);
                    }
                }
                piece.GetComponent<RectTransform>().anchoredPosition = gridList[index].transform.position;
                piece.transform.SetParent(canvasTransform);
                index++;
            }
        }
    }

    private bool CheckSide(int i, int j, int row, int col, ref GameObject obj)
    {
        // Top Side
        if (i == 0 && j != 0 && j != col - 1)
        {
            if (j % 2 != 0)
            {
                obj = Instantiate(T1);
            }

            else
            {
                obj = Instantiate(T2);
            }

            return true;
        }

        // Bottom Side
        else if (i == row - 1 && j != 0 && j != col - 1)
        {
            if (j % 2 != 0)
            {
                obj = Instantiate(B1);
            }
            else
            {
                obj = Instantiate(B2);
            }
            return true;
        }

        // Left Side
        else if (j == 0 && i != 0 && i != col - 1)
        {
            if (i % 2 != 0)
            {
                obj = Instantiate(L1);
            }
            else
            {
                obj = Instantiate(L2);
            }
            return true;
        }

        // Right Side
        else if (j == col - 1 && i != 0 && i != col - 1)
        {
            if (i % 2 != 0)
            {
                obj = Instantiate(R1);
            }
            else
            {
                obj = Instantiate(R2);
            }
            return true;
        }


        return false;
    }

    private bool CheckCorner(int i, int j, int row, int col, ref GameObject obj)
    {
        // Top Left Side
        if (i == 0 && j == 0)
        {
            obj = Instantiate(TL);
            return true;
        }

        // Top Right Side
        else if (i == 0 && j == col - 1)
        {
            obj = Instantiate(TR);
            return true;
        }

        // Bottom Left Side
        else if (j == 0 && i == row - 1)
        {
            obj = Instantiate(BL);
            return true;
        }

        // Bottom Right Side
        else if (j == col - 1 && i == row - 1)
        {
            obj = Instantiate(BR);
            return true;
        }

        return false;
    }
}
