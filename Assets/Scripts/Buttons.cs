using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private int scoreMax;
    [SerializeField] private int maxNumberOfMoves;
    [SerializeField] private int customScore;

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject custom;

    [SerializeField] private TMP_InputField rowsInput;
    [SerializeField] private TMP_InputField columnsInput;
    [SerializeField] private TMP_InputField scoreInput;
    [SerializeField] private TMP_InputField movesInput;
    public void StartLevel()
    {
        LevelManager.instance.widthInput = rows;
        LevelManager.instance.lengthInput = columns;
        LevelManager.instance.scoreMax = scoreMax;
        LevelManager.instance.customScore = customScore;
        LevelManager.instance.maxNumberOfMoves = maxNumberOfMoves;
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenCustom()
    {
        buttons.SetActive(false);
        custom.SetActive(true);
    }

    public void CloseCustom()
    {
        buttons.SetActive(true);
        custom.SetActive(false);
    }

    public void ChangeRows()
    {
        rows = int.Parse(rowsInput.text);
    }

    public void ChangeColumns()
    {
        columns = int.Parse(columnsInput.text);
    }

    public void ChangeMoves()
    {
        maxNumberOfMoves = int.Parse(movesInput.text);
    }

    public void ChangeScore()
    {
        customScore = int.Parse(scoreInput.text); 
    }
}
