using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{

    public int score;
    public static GameManager inst;

    public TextMeshProUGUI  scoreText;

    public void IncremnentScore ()
    {
        score++;
        scoreText.text = "SCORE: " + score;
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
