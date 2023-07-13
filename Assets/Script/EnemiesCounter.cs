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
        
        gameEvents = GameEvents.Instance;
        GameEvents.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyKilled -= OnEnemyKilled;
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
