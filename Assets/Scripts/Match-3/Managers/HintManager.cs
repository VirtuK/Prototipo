using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float hintDelay = 10f;
    private float timer;

    [Header("References")]
    [SerializeField] private TileManager tileManager;

    // Verifica se o tempo para uma nova dica foi alcan�ado
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= hintDelay)
        {
            ShowHint();  
            timer = 0f;
        }
    }

    // Reinicia o temporizador de dica e limpa as dicas exibidas
    public void ResetTimer()
    {
        timer = 0f;
        ClearHints();
    }

    // Encontra um movimento v�lido e destaca as pe�as relacionadas
    private void ShowHint()
    {
        var validMove = tileManager.FindValidMove();
        if (validMove.HasValue)
        {
            HighlightTiles(validMove.Value.Item1, validMove.Value.Item2);
        }
    }

    // Destaca as duas pe�as que formam o movimento v�lido encontrado
    private void HighlightTiles(Tile tileA, Tile tileB)
    {
        tileA.StartHintAnimation();
        tileB.StartHintAnimation();
    }

    // Limpa as anima��es de dica em todas as pe�as
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