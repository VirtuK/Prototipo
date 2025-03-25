using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Texts")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text movesText;

    //Atualiza o texto de Score da UI
    public void UpdateScoreText(int score, int scoreGoal)
    {
        scoreText.text = "Score: " + score + "/" + scoreGoal;
    }

    //Atualiza o texto de movimentos da UI
    public void UpdateMovesText(int moves)
    {
        movesText.text = "Moves Left: " + moves;
    }
}
