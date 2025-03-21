using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    public List<Tile> tiles = new List<Tile>();

    public void CreateTile(int width, int lenght)
    {
        GameObject tile = Instantiate(tilePrefab, this.transform);
        tiles.Add(tile.GetComponent<Tile>());
        Tile component = tile.GetComponent<Tile>();
        component.row = width;
        component.column = lenght;
        Debug.Log("criou"); 
    }
}
