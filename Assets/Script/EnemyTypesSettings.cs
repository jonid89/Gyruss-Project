using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTypesSettings", menuName = "Create Enemy Type Settings")]
public class EnemyTypesSettings : ScriptableObject
{
#region Singleton
    public static EnemyTypesSettings Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
#endregion

    [System.Serializable] public class EnemyType{
        public GameObject enemyPrefab;
        public Sprite enemySprite;
        public Color enemyColor;
        public float enemySize = 1;
        public int shotDamage = 5;
        public float movementSpeed = 100f;
        public int maxHealth = 10;
        public int hitScore = 2;
        public float initialCircleRadius = 0.5f;
        public float circleRadiusIncrement = 0.1f;
        public int minBatchSize = 2; 
        public int maxBatchSize = 10;
        public int batchAmountOnFirstAppearance = 3;
        public int firstLevelAppearance = 1;
        public int batchAmountIncreasePerLevel = 1;
    }

    public List<EnemyType> enemyTypes = new List<EnemyType>();


}
