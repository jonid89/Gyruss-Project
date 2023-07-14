using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private GameEvents gameEvents;
    
    void Start()
    {
        //Subscribe to Game Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnEnemyHit += OnEnemyHit;
        GameEvents.OnGameRestart += ResetScore;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyHit -= OnEnemyHit;
        GameEvents.OnGameRestart -= ResetScore;
    }

    private void OnEnemyHit(int damage)
    {
        score += damage;
        DisplayScore();
    }

    private void ResetScore()
    {
        score = 0;
        DisplayScore();
    }

    private void DisplayScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    

}
