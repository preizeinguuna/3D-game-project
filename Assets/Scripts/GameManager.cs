using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    int score;
    public static GameManager inst;
    public static bool gameStarted = false;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] PlayerMovement playerMovement;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        Time.timeScale = 0;
        gameStarted = false;

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        gameStarted = true;

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
        }

        Debug.Log("Game Started and Score shown!");
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
}