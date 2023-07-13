using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyPrefab; 
    public float batchSpawnInterval = 10f;
    public float enemySpawnInterval = 0.5f;
    public EnemyTypesSettings enemySettings;
    private List<EnemyTypesSettings.EnemyType> enemyTypes = new List<EnemyTypesSettings.EnemyType>();
    public int level = 1;

    private void Start()
    {
        enemyTypes = enemySettings.enemyTypes;

        StartCoroutine(StartEnemySpawning(enemyTypes.Count));
    }

    private void Update()
    {
                
    }

    private IEnumerator StartEnemySpawning(int enemyType)
    {
        //Buffer time before first batch
        yield return new WaitForSeconds(batchSpawnInterval);

        //Iterate through each enemyType to start a batch spawning coroutine
        for(int i=0; i < enemyTypes.Count; i++)
        {
            if( level >= enemyTypes[i].firstLevelAppearance )// && Time.time >= nextBatchSpawnTime)
            {    
                //Starts a coroutine that will spawn a Type of Enemies over time
                StartCoroutine(SpawnEnemyType(i));
                
                yield return new WaitForSeconds(batchSpawnInterval);
            }
        }
    }


    private IEnumerator SpawnEnemyType(int enemyType)
    {
        //Assign variables of enemyType
        GameObject enemyPrefab = enemyTypes[enemyType].enemyPrefab;
        Sprite enemySprite = enemyTypes[enemyType].enemySprite;
        Color enemyColor = enemyTypes[enemyType].enemyColor;
        float enemySize = enemyTypes[enemyType].enemySize;
        int shotDamage = enemyTypes[enemyType].shotDamage;
        float movementSpeed = enemyTypes[enemyType].movementSpeed;
        int maxHealth = enemyTypes[enemyType].maxHealth;
        int hitScore = enemyTypes[enemyType].hitScore;
        float initialCircleRadius = enemyTypes[enemyType].initialCircleRadius;
        float circleRadiusIncrement = enemyTypes[enemyType].circleRadiusIncrement;
        int minBatchSize = enemyTypes[enemyType].minBatchSize;
        int maxBatchSize = enemyTypes[enemyType].maxBatchSize;
        int batchAmountOnFirstAppearance = enemyTypes[enemyType].batchAmountOnFirstAppearance;
        int firstLevelAppearance = enemyTypes[enemyType].firstLevelAppearance;
        int batchAmountIncreasePerLevel = enemyTypes[enemyType].batchAmountIncreasePerLevel;
        
        //Determine amount of batches based on Level
        int batchesCount = 
            batchAmountOnFirstAppearance + 
            (batchAmountIncreasePerLevel * (level - firstLevelAppearance) );

        Debug.Log("enemyType: "+ enemyType + ", batchesCount: "+ batchesCount);

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
                    
                    //Setting variables on Instantiated Enemy
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
                    
                    yield return new WaitForSeconds(enemySpawnInterval);
                }
        }
    }
}
