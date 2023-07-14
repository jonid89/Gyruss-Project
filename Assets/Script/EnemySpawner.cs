using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
#region Variables
    // Configuration variables
    private GameObject enemyPrefab; 
    public float batchSpawnInterval = 10f;
    public float enemySpawnInterval = 0.5f;
    public EnemyTypesSettings enemySettings;

    // Level variables
    private int currentLevel = 1;
    private int totalEnemyCount;
    public int GetTotalEnemyCount()
    { return totalEnemyCount;}
    private bool lastEnemySpawned; 
    public bool GetLastEnemySpawned()
    { return lastEnemySpawned;}

    // Game events and object pooler
    private GameEvents gameEvents;
    private ObjectPooler<EnemyController> objectPooler;

    
    // Enemy types list
    private List<EnemyTypesSettings.EnemyType> enemyTypesListOriginal = new List<EnemyTypesSettings.EnemyType>();
    private List<EnemyTypesSettings.EnemyType> enemyTypesListCopy = new List<EnemyTypesSettings.EnemyType>();

#endregion

    private void Start()
    {
        //Subscribe to Game Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnNextLevel += StartNextLevel;
        GameEvents.OnGameRestart += RestartSpawner;

        //Initializing values
        enemyTypesListOriginal = enemySettings.enemyTypes;
        totalEnemyCount = 0;
        lastEnemySpawned = false;

        //Creating Pools of Enemy Prefabs
        objectPooler = ObjectPooler<EnemyController>.Instance;
        CreateEnemyPools();

        //Making sure to start at level 1
        RestartSpawner();
    }

    private void CreateEnemyPools()
    {
        //Creating Pools of Enemy for each Prefab
        foreach (EnemyTypesSettings.EnemyType originalType in enemyTypesListOriginal)
        {
            objectPooler.CreatePool(originalType.enemyPrefab.GetComponent<EnemyController>(), 100);
        }
    }

    private void OnDestroy()
    {
        enemyTypesListCopy.Clear();
        
        //Unsubscribe to Game Events
        GameEvents.OnNextLevel -= StartNextLevel;
        GameEvents.OnGameRestart += RestartSpawner;
    }

    private void RestartSpawner()
    {
        StartNextLevel(1);
    }

    private void StartNextLevel(int level){
        //Resetting values for next level;
        currentLevel = level;
        lastEnemySpawned = false;
        totalEnemyCount = 0;

        // Cancel any existing coroutines
        StopAllCoroutines();

        //Starting coroutine to Spawn Enemies
        StartCoroutine(StartEnemySpawning());
    }

    private IEnumerator StartEnemySpawning()
    {
        //Copy List to ensure Original List is not modified
        CopyEnemyTypesList();
        
        //Buffer time before first batch
        yield return new WaitForSeconds(batchSpawnInterval);

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
        
        // Create new instances of enemyType and add them to the copy list
        foreach (EnemyTypesSettings.EnemyType originalType in enemyTypesListOriginal)
        {
            if(originalType.firstLevelAppearance <= currentLevel)
            {
                EnemyTypesSettings.EnemyType copiedType = new EnemyTypesSettings.EnemyType
                {
#region Copying type Properties
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
#endregion
                };

                enemyTypesListCopy.Add(copiedType);
            }
        }
    }

    private IEnumerator SpawnEnemyType(int enemyType)
    {
#region EnemyType Properties Declaration
        //Declare properties of enemyType
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
#endregion
        
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
                    EnemyController enemy = objectPooler.GetFromPool(enemyPrefab.GetComponent<EnemyController>());
                    enemy.gameObject.SetActive(true);
                    
                    //Setting properties on Instantiated Enemy
                    enemy.SetConfig(
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

                    //Counting total amount of enemies Pooled for this level
                    totalEnemyCount++;
                    
                    //Checking if this is the last enemy spawned
                    if( (enemyType >= enemyTypesListCopy.Count-1) &&
                        (j >= batchesCount-1) &&
                        (k >= batchSize-1))
                    {
                        lastEnemySpawned = true;
                    }
                
                    yield return new WaitForSeconds(enemySpawnInterval);
                }
        }
    }

}
