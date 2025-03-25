using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("Points Text Settings")]
    [SerializeField] private GameObject pointsTextPrefab;
    [SerializeField] private List<GameObject> pointsText = new List<GameObject>();

    [Header("Selected Tile")]
    private Tile selectedTile;

    [Header("References")]
    [SerializeField] private AnimationController animController;
    public BoardManager boardManager;

    // Seleciona uma peça e ativa a verificação caso já tiver uma peça selecionada
    public void SelectTile(Tile tile)
    {
        if (selectedTile == null)
        {
            selectedTile = tile;
            selectedTile.HighlightSelect();
        }
        else if(selectedTile == tile)
        {
            selectedTile.ResetHighlight();
            selectedTile = null;
        }
        else
        {
            if (GameUtilities.AreAdjacent(selectedTile, tile))
            {
                selectedTile.ResetHighlight();
                StartCoroutine(SwapAndCheck(selectedTile, tile));
            }
            selectedTile = null;
        }
    }

    // Troca as peças e verifica se há combinações
    private IEnumerator SwapAndCheck(Tile tile1, Tile tile2)
    {
        yield return StartCoroutine(animController.SwapItems(tile1, tile2));
        if (!CheckForMatches())
        {
            yield return StartCoroutine(animController.SwapItems(tile1, tile2));
            Debug.Log("No match found, swapping back.");
        }
        else
        {
            StartCoroutine(HandleMatches());
        }
    }

    // Verifica se há combinações no tabuleiro
    public bool CheckForMatches(List<Row> rows, int width, int length)
    {
        bool matchFound = false;
        foreach (var row in rows)
        {
            foreach (var tile in row.tiles)
            {
                tile.isMatched = false;
            }
        }

        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < length - 2; col++)
            {
                Tile currentTile = rows[row].tiles[col];
                Tile nextTile1 = rows[row].tiles[col + 1];
                Tile nextTile2 = rows[row].tiles[col + 2];

                if (currentTile.item != null &&
                    currentTile.item == nextTile1.item &&
                    currentTile.item == nextTile2.item)
                {
                    currentTile.isMatched = true;
                    nextTile1.isMatched = true;
                    nextTile2.isMatched = true;
                    matchFound = true;
                }
            }
        }

        for (int col = 0; col < length; col++)
        {
            for (int row = 0; row < width - 2; row++)
            {
                Tile currentTile = rows[row].tiles[col];
                Tile nextTile1 = rows[row + 1].tiles[col];
                Tile nextTile2 = rows[row + 2].tiles[col];

                if (currentTile.item != null &&
                    currentTile.item == nextTile1.item &&
                    currentTile.item == nextTile2.item)
                {
                    currentTile.isMatched = true;
                    nextTile1.isMatched = true;
                    nextTile2.isMatched = true;
                    matchFound = true;
                }
            }
        }
        return matchFound;
    }

    // Retorna o resultado da verificação
    public bool CheckForMatches()
    {
        return CheckForMatches(boardManager.GetRows(), boardManager.width, boardManager.length);
    }

    // Lida com as combinações, limpando e reorganizando o tabuleiro
    private IEnumerator HandleMatches()
    {
        ClearMatches();
        yield return new WaitForSeconds(0.01f);
        ShiftTilesDown();
        RefillBoard();
    }

    // Limpa as peças combinadas, cria o texto de pontos delas e adiciona os pontos no score
    private void ClearMatches()
    {
        List<Row> rows = boardManager.GetRows();
        for (int i = 0; i < boardManager.width; i++)
        {
            for (int j = 0; j < boardManager.length; j++)
            {
                Tile tile = rows[i].tiles[j];
                if (tile.isMatched)
                {
                    if (tile.item != null)
                    {
                        boardManager.AddScore(tile.item.points);
                        GameObject pt = Instantiate(pointsTextPrefab, tile.transform);
                        pt.GetComponent<TMP_Text>().text = "+" + tile.item.points;
                        pointsText.Add(pt);
                    }
                    tile.item = null;
                    tile.icon.sprite = null;
                }
            }
        }
    }

    // Move as peças que possuem espaços vazios abaixo delas para baixo
    private void ShiftTilesDown()
    {
        List<Row> rows = boardManager.GetRows();
        for (int col = 0; col < boardManager.length; col++)
        {
            for (int row = boardManager.width - 1; row >= 0; row--)
            {
                if (rows[row].tiles[col].item == null)
                {
                    for (int k = row; k > 0; k--)
                    {
                        Tile upperTile = rows[k - 1].tiles[col];
                        Tile currentTile = rows[k].tiles[col];

                        currentTile.item = upperTile.item;
                        currentTile.icon.sprite = upperTile.icon.sprite;
                        upperTile.item = null;
                        upperTile.icon.sprite = null;
                    }
                }
            }
        }
    }

    // Preenche o tabuleiro com novas peças
    private void RefillBoard()
    {
        List<Row> rows = boardManager.GetRows();
        List<Tile> newTiles = new List<Tile>();

        for (int col = 0; col < boardManager.length; col++)
        {
            for (int row = 0; row < boardManager.width; row++)
            {
                Tile tile = rows[row].tiles[col];
                if (tile.item == null)
                {
                    int rdn = Random.Range(0, boardManager.GetItems().Length);
                    tile.item = boardManager.GetItems()[rdn];
                    tile.icon.sprite = boardManager.GetItems()[rdn].sprite;
                    newTiles.Add(tile);
                    StartCoroutine(animController.AnimateNewTile(tile));
                }
            }
        }
        StartCoroutine(WaitAndCheck(newTiles));
    }

    // Aguarda e verifica se há movimentos possíveis, também limpa os textos de pontos da tela
    private IEnumerator WaitAndCheck(List<Tile> newTiles)
    {
        if (!HasPossibleMoves(boardManager.GetRows(), boardManager.width, boardManager.length))
        {
            Debug.Log("No possible moves! Re-randomizing new tiles...");
            RegenerateNewTiles(newTiles);
        }
        yield return new WaitForSeconds(0.3f);

        foreach (var pt in pointsText)
        {
            Destroy(pt);
        }
        pointsText.Clear();

        yield return new WaitForSeconds(0.8f);
        boardManager.DecrementMoves();

        if (CheckForMatches())
        {
            StartCoroutine(HandleMatches());
        }
    }

    // Verifica se há movimentos possíveis no tabuleiro
    public bool HasPossibleMoves(List<Row> rows, int width, int length)
    {
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < length; col++)
            {
                Tile currentTile = rows[row].tiles[col];

                if (col < length - 1)
                {
                    Tile rightTile = rows[row].tiles[col + 1];
                    GameUtilities.SwapTiles(currentTile, rightTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, rightTile);
                        return true;
                    }
                    GameUtilities.SwapTiles(currentTile, rightTile);
                }
                if (col > 0)
                {
                    Tile leftTile = rows[row].tiles[col - 1];
                    GameUtilities.SwapTiles(currentTile, leftTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, leftTile);
                        return true;
                    }
                    GameUtilities.SwapTiles(currentTile, leftTile);
                }
                if (row < width - 1)
                {
                    Tile bottomTile = rows[row + 1].tiles[col];
                    GameUtilities.SwapTiles(currentTile, bottomTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, bottomTile);
                        return true;
                    }
                    GameUtilities.SwapTiles(currentTile, bottomTile);
                }
                if (row > 0)
                {
                    Tile topTile = rows[row - 1].tiles[col];
                    GameUtilities.SwapTiles(currentTile, topTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, topTile);
                        return true;
                    }
                    GameUtilities.SwapTiles(currentTile, topTile);
                }
            }
        }
        return false;
    }

    //Retorna o resultado da verificação
    public bool HasPossibleMoves()
    {
        return HasPossibleMoves(boardManager.GetRows(), boardManager.width, boardManager.length);
    }

    //Recria as novas peças do tabuleiro e garante que sempre tenha uma combinação possivel pelo menos
    private void RegenerateNewTiles(List<Tile> newTiles)
    {
        foreach (Tile tile in newTiles)
        {
            int rdn = Random.Range(0, boardManager.GetItems().Length);
            tile.item = boardManager.GetItems()[rdn];
            tile.icon.sprite = boardManager.GetItems()[rdn].sprite;
        }
        if (!HasPossibleMoves())
        {
            Debug.Log("Still no moves! Regenerating new tiles again...");
            RegenerateNewTiles(newTiles);
        }
    }

    //Verifica se existe algum movimento válido no jogo
    public (Tile, Tile)? FindValidMove()
    {
        List<Row> rows = boardManager.GetRows();
        int width = boardManager.width;
        int length = boardManager.length;

        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < length; col++)
            {
                Tile currentTile = rows[row].tiles[col];

                if (col < length - 1)
                {
                    Tile rightTile = rows[row].tiles[col + 1];
                    GameUtilities.SwapTiles(currentTile, rightTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, rightTile);
                        return (currentTile, rightTile);
                    }
                    GameUtilities.SwapTiles(currentTile, rightTile);
                }
                
                if (col > 0)
                {
                    Tile leftTile = rows[row].tiles[col - 1];
                    GameUtilities.SwapTiles(currentTile, leftTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, leftTile);
                        return (currentTile, leftTile);
                    }
                    GameUtilities.SwapTiles(currentTile, leftTile);
                }
                
                if (row < width - 1)
                {
                    Tile bottomTile = rows[row + 1].tiles[col];
                    GameUtilities.SwapTiles(currentTile, bottomTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, bottomTile);
                        return (currentTile, bottomTile);
                    }
                    GameUtilities.SwapTiles(currentTile, bottomTile);
                }
               
                if (row > 0)
                {
                    Tile topTile = rows[row - 1].tiles[col];
                    GameUtilities.SwapTiles(currentTile, topTile);
                    if (CheckForMatches())
                    {
                        GameUtilities.SwapTiles(currentTile, topTile);
                        return (currentTile, topTile);
                    }
                    GameUtilities.SwapTiles(currentTile, topTile);
                }
            }
        }
        return null;
    }
}
