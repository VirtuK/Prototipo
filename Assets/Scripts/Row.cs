using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    public List<Tile> tiles = new List<Tile>();

    public void CreateTile()
    {
        GameObject tile = Instantiate(tilePrefab, this.transform);
        tiles.Add(tile.GetComponent<Tile>());
    }
}
