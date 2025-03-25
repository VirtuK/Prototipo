using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Row : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField] private GameObject tilePrefab;
    public List<Tile> tiles = new List<Tile>();

    // Cria uma pe�a e adiciona � lista de pe�as
    public void CreateTile(int width, int lenght)
    {
        GameObject tile = Instantiate(tilePrefab, this.transform);
        tiles.Add(tile.GetComponent<Tile>());
        Tile component = tile.GetComponent<Tile>();
        component.row = width;
        component.column = lenght;
    }
}
