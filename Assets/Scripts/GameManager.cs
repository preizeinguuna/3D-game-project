using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score;
    int highScore;

    public static GameManager inst;
    public static bool gameStarted = false;

    [Header("Gameplay UI")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Start Screen")]
    [SerializeField] GameObject startGameScreen;

    [Header("Game Over Screen")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    [Header("Player")]
    [SerializeField] PlayerMovement playerMovement;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        Time.timeScale = 0f;
        gameStarted = false;
        score = 0;

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (scoreText != null)
        {
            scoreText.text = "SCORE: 0";
            scoreText.gameObject.SetActive(false);
        }

        if (startGameScreen != null)
        {
            startGameScreen.SetActive(true);
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        gameStarted = true;

        if (startGameScreen != null)
        {
            startGameScreen.SetActive(false);
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
        }
    }

    public void IncrementScore()
    {
        score++;

        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
        }

        if (playerMovement != null)
        {
            playerMovement.speed += playerMovement.speedIncreasePerPoint;
        }
    }

    public void ShowGameOver()
    {
        gameStarted = false;
        Time.timeScale = 0f;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "SCORE: " + score;
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}