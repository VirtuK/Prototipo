using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int row;
    public int column;
    public Item item;
    public Image icon;
    public bool isMatched;
    private TileManager tileManager;

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    public void Select()
    {
        tileManager.SelectTile(this);
    }
}
