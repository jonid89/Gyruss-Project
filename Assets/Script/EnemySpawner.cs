using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float batchSpawnInterval = 5f;
    public int minBatchSize = 2; 
    public int maxBatchSize = 10;
    public int batchAmount = 3;
    public float enemySpawnInterval = 0.5f;
    private float nextBatchSpawnTime;
    

    private void Start()
    {
        nextBatchSpawnTime = Time.time + batchSpawnInterval;
    }

    private void Update()
    {
        // Checking if it's time for another batch of enemies and if there are batches left to spawn
        if (Time.time >= nextBatchSpawnTime && batchAmount > 0)
        {
            batchAmount-=1;
            int batchSize = Random.Range(minBatchSize, maxBatchSize);
            
            //Starts a coroutine that will spawn a batch of Enemies over time
            StartCoroutine(SpawnBatch(batchSize));
            nextBatchSpawnTime = Time.time + batchSpawnInterval;
        }
    }

    private IEnumerator SpawnBatch(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position;

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }
}
