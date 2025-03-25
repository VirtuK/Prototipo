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

    // Verifica se o tempo para uma nova dica foi alcançado
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

    // Encontra um movimento válido e destaca as peças relacionadas
    private void ShowHint()
    {
        var validMove = tileManager.FindValidMove();
        if (validMove.HasValue)
        {
            HighlightTiles(validMove.Value.Item1, validMove.Value.Item2);
        }
    }

    // Destaca as duas peças que formam o movimento válido encontrado
    private void HighlightTiles(Tile tileA, Tile tileB)
    {
        tileA.StartHintAnimation();
        tileB.StartHintAnimation();
    }

    // Limpa as animações de dica em todas as peças
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