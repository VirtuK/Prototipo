using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text movesText;

    public void UpdateScoreText(int score, int scoreGoal)
    {
        scoreText.text = "Score: " + score + "/" + scoreGoal;
    }

    public void UpdateMovesText(int moves)
    {
        movesText.text = "Moves Left: " + moves;
    }
}
