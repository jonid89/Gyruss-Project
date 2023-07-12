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
        Instance = this;
    }
#endregion

    public delegate void EnemyHit(int damage);
    public static EnemyHit OnEnemyHit;

    public static void Initialize()
    {
        Instance = new GameEvents();
    }

    public void TriggerEnemyHit(int damage)
    {
        Debug.Log("TriggerEnemyHit Event called with damage: " + damage);
        if(OnEnemyHit != null)
            OnEnemyHit(damage);
    }

}


