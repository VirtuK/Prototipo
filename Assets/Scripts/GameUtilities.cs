using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static bool AreAdjacent(Tile tile1, Tile tile2)
    {
        return (Mathf.Abs(tile1.row - tile2.row) == 1 && tile1.column == tile2.column) ||
               (Mathf.Abs(tile1.column - tile2.column) == 1 && tile1.row == tile2.row);
    }

    public static void SwapTiles(Tile tile1, Tile tile2)
    {
        Item tempItem = tile1.item;
        tile1.item = tile2.item;
        tile2.item = tempItem;

        Sprite tempSprite = tile1.icon.sprite;
        tile1.icon.sprite = tile2.icon.sprite;
        tile2.icon.sprite = tempSprite;
    }
}
