using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private List<Row> rows = new List<Row>();
    [SerializeField] private GameObject RowPrefab;

    public Tile selectedTile;


    public int width;
    public int length;

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
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
        CheckForMatches();
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
                SwapItems(selectedTile, tile);
                if (!CheckForMatches())
                {
                    SwapItems(selectedTile, tile);
                    print("dont");
                }
                else
                {
                    HandleMatches();
                }
            }
            selectedTile = null;
        }
    }

    private bool AreAdjacent(Tile tile1, Tile tile2)
    {
        return (Mathf.Abs(tile1.row - tile2.row) == 1 && tile1.column == tile2.column) ||
               (Mathf.Abs(tile1.column - tile2.column) == 1 && tile1.row == tile2.row);
    }

    private void SwapItems(Tile tile1, Tile tile2)
    {
        Item tempItem = tile1.item;
        tile1.item = tile2.item;
        tile2.item = tempItem;

        tile1.icon.sprite = tile1.item.sprite;
        tile2.icon.sprite = tile2.item.sprite;
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

   private void HandleMatches()
    {
        ClearMatches();
        ShiftTilesDown();
        RefillBoard();
        CheckForMatches();
    }

    private void ClearMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (rows[i].tiles[j].isMatched)
                {
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
                    for (int k = row - 1; k >= 0; k--)
                    {
                        if (rows[k].tiles[col].item != null)
                        {
                            rows[row].tiles[col].item = rows[k].tiles[col].item;
                            rows[row].tiles[col].icon.sprite = rows[k].tiles[col].icon.sprite;

                            rows[k].tiles[col].item = null;
                            rows[k].tiles[col].icon.sprite = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (rows[i].tiles[j].item == null)
                {
                    int rdn = Random.Range(0, items.Length);
                    rows[i].tiles[j].item = items[rdn];
                    rows[i].tiles[j].icon.sprite = items[rdn].sprite;
                }
            }
        }
    }
}
