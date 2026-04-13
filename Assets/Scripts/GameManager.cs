using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    int score;
    public static GameManager inst;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] PlayerMovement playerMovement;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        // Freeze the game time
        Time.timeScale = 0;

        // Hide the score text when the menu is visible
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        // Resume the game time
        Time.timeScale = 1;

        // Show the score text when the game starts
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
        }

        Debug.Log("Game Started and Score shown!");
    }

    public void IncremnentScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }
}