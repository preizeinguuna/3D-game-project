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

    public void IncremnentScore ()
    {
        score++;
        scoreText.text = "SCORE: " + score;
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    private void Awake ()
    {
        inst = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
