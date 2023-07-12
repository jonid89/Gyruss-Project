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
        gameEvents = GameEvents.Instance;
        
        GameEvents.OnEnemyHit += OnEnemyHit;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyHit -= OnEnemyHit;
    }

    private void OnEnemyHit(int damage)
    {
        Debug.Log("Enemy hit! Damage: " + damage);
        score += damage;
        scoreText.text = "Score: " + score.ToString();
    }

}
