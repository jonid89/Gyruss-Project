using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
#region Singleton
    public static GameEvents Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else Destroy(this);
    }
#endregion

    public delegate void EnemyHit(int damage);
    public static EnemyHit OnEnemyHit;

    public delegate void NextLevel(int level);
    public static NextLevel OnNextLevel;

    public delegate void EnemyKilled();
    public static EnemyKilled OnEnemyKilled;

    public delegate void LastEnemyKilled();
    public static LastEnemyKilled OnLastEnemyKilled;
    
    public delegate void PlayerDamaged();
    public static PlayerDamaged OnPlayerDamaged;
    
    public delegate void GameOver();
    public static GameOver OnGameOver;

    public delegate void GameRestart();
    public static GameRestart OnGameRestart;
    
    public static void Initialize()
    {
        Instance = new GameEvents();
    }

    public void TriggerEnemyHit(int damage)
    {
        if(OnEnemyHit != null)
            OnEnemyHit(damage);
    }

    public void TriggerNextLevel(int level)
    {
        if(OnNextLevel != null)
            OnNextLevel(level);
    }

    public void TriggerEnemyKilled()
    {
        if(OnEnemyKilled != null)
            OnEnemyKilled();
    }

    public void TriggerLastEnemyKilled()
    {
        if(OnLastEnemyKilled != null)
            OnLastEnemyKilled();
    }

    public void TriggerPlayerDamaged()
    {
        if(OnPlayerDamaged != null)
            OnPlayerDamaged();
    }

    public void TriggerGameOver()
    {
        if(OnGameOver != null)
            OnGameOver();
    }

    public void TriggerGameRestart()
    {
        if(OnGameRestart != null)
            OnGameRestart();
    }

}


