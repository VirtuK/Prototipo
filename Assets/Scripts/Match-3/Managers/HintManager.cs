using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public float hintDelay = 10f; 
    private float timer;

    public TileManager tileManager; 

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= hintDelay)
        {
            ShowHint();
            timer = 0f; 
        }
    }

    public void ResetTimer()
    {
        timer = 0f;
        ClearHints();
    }

    private void ShowHint()
    {
        var validMove = tileManager.FindValidMove();
        if (validMove.HasValue)
        {
            HighlightTiles(validMove.Value.Item1, validMove.Value.Item2);
        }
    }

    private void HighlightTiles(Tile tileA, Tile tileB)
    {
        tileA.StartHintAnimation();
        tileB.StartHintAnimation();
    }

    private void ClearHints()
    {
        foreach (Row row in tileManager.boardManager.GetRows())
        {
            foreach (Tile tile in row.tiles)
            {
                tile.ClearHintAnimation();
            }
        }
    }
}
