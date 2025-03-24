using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUiButtons : MonoBehaviour
{
    public GameObject victoryScreen;
    public GameObject gameOverScreen;

    [SerializeField] private TMP_Text victoryScoreText;
    [SerializeField] private TMP_Text gameOverScoreText;

    public void VictoryScreen(int score)
    {
        victoryScreen.SetActive(true);
        UpdateScoreText(victoryScoreText, score);
    }

    public void GameOverScreen(int score)
    {
        gameOverScreen.SetActive(true);
        UpdateScoreText(gameOverScoreText, score);
    }

    public void UpdateScoreText(TMP_Text txt, int score)
    {
        txt.text = "Score: " + score;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
