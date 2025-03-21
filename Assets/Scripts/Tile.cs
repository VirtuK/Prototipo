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
    private Board board;

    private void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void Select()
    {
        board.SelectTile(this);
    }
}
