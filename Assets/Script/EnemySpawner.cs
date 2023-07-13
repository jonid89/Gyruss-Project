using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyPrefab; 
    public float batchSpawnInterval = 10f;
    public float enemySpawnInterval = 0.5f;
    public EnemyTypesSettings enemySettings;
    private int currentLevel = 1;
    private int enemyTypesToSpawn;
    private int totalEnemyCount;
    public int GetTotalEnemyCount()
    { return totalEnemyCount;}
    private bool lastEnemySpawned; 
    public bool GetLastEnemySpawned()
    { return lastEnemySpawned;}
    private GameEvents gameEvents;
    private List<EnemyTypesSettings.EnemyType> enemyTypesListOriginal = new List<EnemyTypesSettings.EnemyType>();
    private List<EnemyTypesSettings.EnemyType> enemyTypesListCopy = new List<EnemyTypesSettings.EnemyType>();
/*
#region enemyTypeProperties
            private GameObject enemyPrefab = enemyTypes[enemyType].enemyPrefab;
            private Sprite enemySprite = enemyTypes[enemyType].enemySprite;
            private Color enemyColor = enemyTypes[enemyType].enemyColor;
            private float enemySize = enemyTypes[enemyType].enemySize;
            private int shotDamage = enemyTypes[enemyType].shotDamage;
            private float movementSpeed = enemyTypes[enemyType].movementSpeed;
            private int maxHealth = enemyTypes[enemyType].maxHealth;
            private int hitScore = enemyTypes[enemyType].hitScore;
            private float initialCircleRadius = enemyTypes[enemyType].initialCircleRadius;
            private float circleRadiusIncrement = enemyTypes[enemyType].circleRadiusIncrement;
            private int minBatchSize = enemyTypes[enemyType].minBatchSize;
            private int maxBatchSize = enemyTypes[enemyType].maxBatchSize;
            private int batchAmountOnFirstAppearance = enemyTypes[enemyType].batchAmountOnFirstAppearance;
            private int firstLevelAppearance = enemyTypes[enemyType].firstLevelAppearance;
            private int batchAmountIncreasePerLevel = enemyTypes[enemyType].batchAmountIncreasePerLevel;
#endregion
*/


    private void Start()
    {
        enemyTypesListOriginal = enemySettings.enemyTypes;
        
        totalEnemyCount = 0;
        lastEnemySpawned = false;
        
        gameEvents = GameEvents.Instance;
        GameEvents.OnNextLevel += StartNextLevel;

        currentLevel = 1;
        StartNextLevel(1);
    }

    private void OnDestroy()
    {
        GameEvents.OnNextLevel -= StartNextLevel;
    }

    private void StartNextLevel(int level){
        currentLevel = level;
        StartCoroutine(StartEnemySpawning());

    }

    private IEnumerator StartEnemySpawning()
    {
        //Copy List to ensure Original List is not modified
        CopyEnemyTypesList();
        
        //Buffer time before first batch
        yield return new WaitForSeconds(batchSpawnInterval);

        //Removing Types that are not spawned in this level        
        /*foreach (EnemyTypesSettings.EnemyType type in enemyTypesListOriginal)
        {
            if(type.firstLevelAppearance > currentLevel)
            {
                enemyTypesListCopy.Remove(type);
            }
        }*/

        //Iterate through each enemyType to start a batch spawning coroutine
        for(int i=0; i < enemyTypesListCopy.Count; i++)
        {
                //Starts a coroutine that will spawn a Type of Enemies over time
                StartCoroutine(SpawnEnemyType(i));
                
                yield return new WaitForSeconds(batchSpawnInterval);
        }
    }

    private void CopyEnemyTypesList()
    {
        // Clear the copy list to start with a fresh copy
        enemyTypesListCopy.Clear();

        // Create new instances and add them to the copy list
        foreach (EnemyTypesSettings.EnemyType originalType in enemyTypesListOriginal)
        {
            if(originalType.firstLevelAppearance <= currentLevel)
            {
                EnemyTypesSettings.EnemyType copiedType = new EnemyTypesSettings.EnemyType
                {
                    enemyPrefab = originalType.enemyPrefab,
                    enemySprite = originalType.enemySprite,
                    enemyColor = originalType.enemyColor,
                    enemySize = originalType.enemySize,
                    shotDamage = originalType.shotDamage,
                    movementSpeed = originalType.movementSpeed,
                    maxHealth = originalType.maxHealth,
                    hitScore = originalType.hitScore,
                    initialCircleRadius = originalType.initialCircleRadius,
                    circleRadiusIncrement = originalType.circleRadiusIncrement,
                    minBatchSize = originalType.minBatchSize,
                    maxBatchSize = originalType.maxBatchSize,
                    batchAmountOnFirstAppearance = originalType.batchAmountOnFirstAppearance,
                    firstLevelAppearance = originalType.firstLevelAppearance,
                    batchAmountIncreasePerLevel = originalType.batchAmountIncreasePerLevel
                };

                enemyTypesListCopy.Add(copiedType);
            }
        }
    }




    private IEnumerator SpawnEnemyType(int enemyType)
    {
        //Assigning properties of enemyType
        GameObject enemyPrefab = enemyTypesListCopy[enemyType].enemyPrefab;
        Sprite enemySprite = enemyTypesListCopy[enemyType].enemySprite;
        Color enemyColor = enemyTypesListCopy[enemyType].enemyColor;
        float enemySize = enemyTypesListCopy[enemyType].enemySize;
        int shotDamage = enemyTypesListCopy[enemyType].shotDamage;
        float movementSpeed = enemyTypesListCopy[enemyType].movementSpeed;
        int maxHealth = enemyTypesListCopy[enemyType].maxHealth;
        int hitScore = enemyTypesListCopy[enemyType].hitScore;
        float initialCircleRadius = enemyTypesListCopy[enemyType].initialCircleRadius;
        float circleRadiusIncrement = enemyTypesListCopy[enemyType].circleRadiusIncrement;
        int minBatchSize = enemyTypesListCopy[enemyType].minBatchSize;
        int maxBatchSize = enemyTypesListCopy[enemyType].maxBatchSize;
        int batchAmountOnFirstAppearance = enemyTypesListCopy[enemyType].batchAmountOnFirstAppearance;
        int firstLevelAppearance = enemyTypesListCopy[enemyType].firstLevelAppearance;
        int batchAmountIncreasePerLevel = enemyTypesListCopy[enemyType].batchAmountIncreasePerLevel;
        
        //Determine amount of batches based on Level
        int batchesCount = batchAmountOnFirstAppearance + 
                  (batchAmountIncreasePerLevel * (currentLevel - firstLevelAppearance));

        //Iterate batchesCount
        for (int j = 0; j < batchesCount; j++)
        {
            //Determine batch size
            int batchSize = Random.Range(minBatchSize,maxBatchSize);

            //Iterate batchSize to Instantiate Enemy
            for (int k = 0; k < batchSize; k++)
                {
                    GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    EnemyController enemyController = enemy.gameObject.GetComponent<EnemyController>();
                    
                    //Setting properties on Instantiated Enemy
                    enemyController.SetConfig(
                        enemySprite,
                        enemyColor,
                        enemySize,
                        shotDamage,
                        movementSpeed,
                        maxHealth,
                        hitScore,
                        initialCircleRadius,
                        circleRadiusIncrement
                        );

                    totalEnemyCount++;
                    
                    //Checking if this is the last enemy spawned
                    if( (enemyType >= enemyTypesListCopy.Count-1) &&
                        (j >= batchesCount-1) &&
                        (k >= batchSize-1))
                    {
                        lastEnemySpawned = true;
                        Debug.Log("lastEnemySpawned: "+lastEnemySpawned);
                    }
                
                    yield return new WaitForSeconds(enemySpawnInterval);
                }
        }
    }

}
