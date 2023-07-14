using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCounter : MonoBehaviour
{
    private int totalKilledEnemies = 0;
    private GameEvents gameEvents;
    EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        
        //Subscribe to Game Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnEnemyKilled += OnEnemyKilled;
        GameEvents.OnGameRestart += OnGameRestart;
        GameEvents.OnNextLevel += OnNextLevel;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyKilled -= OnEnemyKilled;
        GameEvents.OnGameRestart -= OnGameRestart;
        GameEvents.OnNextLevel -= OnNextLevel;
    }

    private void OnGameRestart()
    {
        ResetEnemiesCounter();
    }

    private void OnNextLevel(int level)
    {
        ResetEnemiesCounter();
    }

    private void ResetEnemiesCounter()
    {
        totalKilledEnemies = 0;
    }

    private void OnEnemyKilled()
    {
        totalKilledEnemies++;
        CheckIfLastEnemy();
    }

    public void CheckIfLastEnemy()
    {
        if( (enemySpawner.GetLastEnemySpawned() == true ) &&
            (enemySpawner.GetTotalEnemyCount() == totalKilledEnemies ))
        {
            gameEvents.TriggerLastEnemyKilled();
        }
    }
}
