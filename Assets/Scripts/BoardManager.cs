using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
public class BoardManager : MonoBehaviour
{


    [Header("Board Setup")]
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject rowPrefab;

    [Header("Level Settings")]
    public int width;
    public int length;
    public int scoreGoal;
    public int score;
    public int numberOfMoves;
    private int scoreMax;

    [Header("References")]
    public TileManager tileManager;
    public UIManager uiManager;

    private List<Row> rows = new List<Row>();

    private void Start()
    {
        width = LevelManager.instance.widthInput;
        length = LevelManager.instance.lengthInput;
        scoreMax = LevelManager.instance.scoreMax;
        numberOfMoves = LevelManager.instance.maxNumberOfMoves;
        score = 0;

        scoreGoal = (LevelManager.instance.customScore > 0) ? LevelManager.instance.customScore : Random.Range(1, scoreMax) * 10;

        GenerateBoard();
    }

    public void GenerateBoard()
    {
        score = 0;
        for (int i = 1; i <= width; i++)
        {
            GameObject rowObj = Instantiate(rowPrefab, transform);
            Row rowComponent = rowObj.GetComponent<Row>();
            rowObj.name = "Row_" + i;
            rows.Add(rowComponent);
            for (int j = 0; j < length; j++)
            {
                rowComponent.CreateTile(i - 1, j);
                int rdn = Random.Range(0, items.Length);
                rowComponent.tiles[j].item = items[rdn];
                rowComponent.tiles[j].icon.sprite = items[rdn].sprite;
            }
        }

        if (tileManager.CheckForMatches(rows, width, length) || !tileManager.HasPossibleMoves(rows, width, length))
        {
            ResetBoard();
        }
    }

    public void ResetBoard()
    {
        StopAllCoroutines();
        foreach (Row row in rows)
        {
            foreach (Tile tile in row.tiles)
            {
                Destroy(tile.gameObject);
            }
            row.tiles.Clear();
            Destroy(row.gameObject);
        }
        rows.Clear();
        GenerateBoard();
        Debug.Log("Board has been reset");
    }

    private void Update()
    {
        uiManager.UpdateScoreText(score, scoreGoal);
        uiManager.UpdateMovesText(numberOfMoves);
    }

    public void DecrementMoves()
    {
        numberOfMoves--;
        if (score >= scoreGoal || numberOfMoves <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public List<Row> GetRows()
    {
        return rows;
    }

    public Item[] GetItems()
    {
        return items;
    }
}