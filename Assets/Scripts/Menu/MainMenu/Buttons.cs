using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private int scoreMax;
    [SerializeField] private int maxNumberOfMoves;
    [SerializeField] private int customScore;

    [Header("Scene Objects")]
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject custom;

    [Header("Custom Inputs")]
    [SerializeField] private TMP_InputField rowsInput;
    [SerializeField] private TMP_InputField columnsInput;
    [SerializeField] private TMP_InputField scoreInput;
    [SerializeField] private TMP_InputField movesInput;

    // Inicia o n�vel, definindo as configura��es e carregando a cena principal
    public void StartLevel()
    {
        LevelManager.instance.widthInput = rows;
        LevelManager.instance.lengthInput = columns;
        LevelManager.instance.scoreMax = scoreMax;
        LevelManager.instance.customScore = customScore;
        LevelManager.instance.maxNumberOfMoves = maxNumberOfMoves;
        SceneManager.LoadScene("SampleScene");
    }

    // Abre o menu de personaliza��o de n�vel
    public void OpenCustom()
    {
        buttons.SetActive(false);
        custom.SetActive(true);
    }

    // Fecha o menu de personaliza��o e retorna ao menu principal
    public void CloseCustom()
    {
        buttons.SetActive(true);
        custom.SetActive(false);
    }

    // Atualiza o n�mero de linhas com o valor inserido pelo jogador
    public void ChangeRows()
    {
        rows = int.Parse(rowsInput.text);
    }

    // Atualiza o n�mero de colunas com o valor inserido pelo jogador
    public void ChangeColumns()
    {
        columns = int.Parse(columnsInput.text);
    }

    // Atualiza o n�mero m�ximo de movimentos com o valor inserido pelo jogador
    public void ChangeMoves()
    {
        maxNumberOfMoves = int.Parse(movesInput.text);
    }

    // Atualiza a pontua��o personalizada com o valor inserido pelo jogador
    public void ChangeScore()
    {
        customScore = int.Parse(scoreInput.text);
    }

    //Fecha o Jogo
    public void CloseGame()
    {
        Application.Quit();
    }
}