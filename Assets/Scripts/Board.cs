using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] private List<Row> rows = new List<Row>();
    [SerializeField] private GameObject RowPrefab;

    public int width;
    public int lenght;

    private void Start()
    {
        for(int i = 1; i <= width; i++)
        {
            GameObject row = Instantiate(RowPrefab, this.transform);
            Row rowComponent = row.GetComponent<Row>();
            row.name = "Row_" + i;
            rows.Add(rowComponent);
            for (int j = 1; j <= lenght; j++)
            {
                rowComponent.CreateTile();
            }
        }
    }
}
