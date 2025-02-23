using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Show Game Over UI or Restart the game
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}
