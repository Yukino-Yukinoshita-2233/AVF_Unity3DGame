using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPuzzle : MonoBehaviour
{
    [SerializeField]
    private Texture2D Puzzle;

    [ContextMenu("Load Puzzle")]
    public void LoadPuzzle()
    {
        if (Puzzle != null)
        {
            PuzzleManager.Instance.LoadTexture(Puzzle);
        }
    }
}
