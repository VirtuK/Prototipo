using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUiButtons : MonoBehaviour
{
    [Header("Scene Objects")]
    public GameObject victoryScreen;
    public GameObject gameOverScreen;

    [Header("Score Texts")]
    [SerializeField] private TMP_Text victoryScoreText;
    [SerializeField] private TMP_Text gameOverScoreText;

    // Exibe a tela de vitória e atualiza a pontuação final
    public void VictoryScreen(int score)
    {
        victoryScreen.SetActive(true);
        UpdateScoreText(victoryScoreText, score);
    }

    // Exibe a tela de game over e atualiza a pontuação final
    public void GameOverScreen(int score)
    {
        gameOverScreen.SetActive(true);
        UpdateScoreText(gameOverScoreText, score);
    }

    // Atualiza o texto de pontuação exibido na tela de vitória ou game over
    private void UpdateScoreText(TMP_Text txt, int score)
    {
        txt.text = "Score: " + score;
    }

    // Retorna o jogador ao menu principal
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}