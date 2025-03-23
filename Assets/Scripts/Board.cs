using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private List<Row> rows = new List<Row>();
    [SerializeField] private GameObject RowPrefab;
    [SerializeField] private TMP_Text scoreText;

    public Tile selectedTile;


    public int width;
    public int length;

    public int score;
    public int scoreGoal;

    private void Start()
    {
        GenerateBoard();
        
    }

    private void GenerateBoard()
    {
        score = 0;
        scoreGoal = 0;
        scoreGoal = Random.Range(1, 10);
        scoreGoal *= 10;
        for (int i = 1; i <= width; i++)
        {
            GameObject row = Instantiate(RowPrefab, this.transform);
            Row rowComponent = row.GetComponent<Row>();
            row.name = "Row_" + i;
            rows.Add(rowComponent);
            for (int j = 0; j < length; j++)
            {
                rowComponent.CreateTile(i - 1, j);
                int rdn = Random.Range(0, items.Length);
                rowComponent.tiles[j].item = items[rdn];
                rowComponent.tiles[j].icon.sprite = items[rdn].sprite;
            }
        }
        if(CheckForMatches()) ResetBoard();
    }

    private void Update()
    {
        scoreText.text = "Score: " + score + "/" + scoreGoal;
       
    }
    private void ResetBoard()
    {
        for(int i = 0; i < rows.Count; i++)
        {
            for(int j = 0; j < rows[i].tiles.Count; j++)
            {
                Destroy(rows[i].tiles[j].gameObject);
            }
            rows[i].tiles.Clear();
            Destroy(rows[i].gameObject);
        }
        rows.Clear();
        GenerateBoard();
        Debug.Log("Foi reiniciado");

    }

    public void SelectTile(Tile tile)
    {
        if (selectedTile == null)
        {
            selectedTile = tile;
        }
        else
        {
            if (AreAdjacent(selectedTile, tile))
            {
                StartCoroutine(SwapAndCheck(selectedTile, tile));
            }
            selectedTile = null;
        }
    }


    private bool AreAdjacent(Tile tile1, Tile tile2)
    {
        return (Mathf.Abs(tile1.row - tile2.row) == 1 && tile1.column == tile2.column) ||
               (Mathf.Abs(tile1.column - tile2.column) == 1 && tile1.row == tile2.row);
    }

    private IEnumerator SwapItems(Tile tile1, Tile tile2)
    {
        Item tempItem = tile1.item;
        tile1.item = tile2.item;
        tile2.item = tempItem;

        Sprite tempSprite = tile1.icon.sprite;
        tile1.icon.sprite = tile2.icon.sprite;
        tile2.icon.sprite = tempSprite;

        RectTransform icon1Transform = tile1.icon.GetComponent<RectTransform>();
        RectTransform icon2Transform = tile2.icon.GetComponent<RectTransform>();

        Vector3 icon1StartPos = icon1Transform.anchoredPosition;
        Vector3 icon2StartPos = icon2Transform.anchoredPosition;

        float duration = 0.3f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            icon1Transform.anchoredPosition = Vector3.Lerp(icon1StartPos, icon2StartPos, t);
            icon2Transform.anchoredPosition = Vector3.Lerp(icon2StartPos, icon1StartPos, t);
            yield return null;
        }

        icon1Transform.anchoredPosition = icon2StartPos;
        icon2Transform.anchoredPosition = icon1StartPos;

        icon1Transform.anchoredPosition = Vector3.zero;
        icon2Transform.anchoredPosition = Vector3.zero;
    }

    private bool CheckForMatches()
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

    private IEnumerator SwapAndCheck(Tile tile1, Tile tile2)
    {
        yield return StartCoroutine(SwapItems(tile1, tile2));
        if (!CheckForMatches())
        {
            yield return StartCoroutine(SwapItems(tile1, tile2));
            Debug.Log("No match found, swapping back.");
        }
        else
        {
            // Handle matches
            HandleMatches();
        }
    }

    private void HandleMatches()
    {
        ClearMatches();
        ShiftTilesDown();
        RefillBoard();


    }

    private void ClearMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (rows[i].tiles[j].isMatched)
                {
                    score += rows[i].tiles[j].item.points;
                    rows[i].tiles[j].item = null;
                    rows[i].tiles[j].icon.sprite = null;
                }
            }
        }
    }

    private void ShiftTilesDown()
    {
        for (int col = 0; col < length; col++)
        {
            for (int row = width - 1; row >= 0; row--)
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


    private void RefillBoard()
    {
        for (int col = 0; col < length; col++)
        {
            for (int row = 0; row < width; row++)
            {
                if (rows[row].tiles[col].item == null)
                {
                    int rdn = Random.Range(0, items.Length);
                    rows[row].tiles[col].item = items[rdn];
                    rows[row].tiles[col].icon.sprite = items[rdn].sprite;

                    StartCoroutine(AnimateNewTile(rows[row].tiles[col]));
                    StartCoroutine(WaitAndCheck());
                    
                }
            }
        }
    }


    private IEnumerator AnimateNewTile(Tile tile)
    {
        Vector3 originalScale = tile.transform.localScale;
        tile.transform.localScale = Vector3.zero;

        float duration = 0.3f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            tile.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            yield return null;
        }

        tile.transform.localScale = originalScale;
        

    }

    private IEnumerator WaitAndCheck()
    {
        yield return new WaitForSeconds(1);
        if (CheckForMatches()) HandleMatches();
        if (score >= scoreGoal)
        {
            ResetBoard();
        }
    }
}
