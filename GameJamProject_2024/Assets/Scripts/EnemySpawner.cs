using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform target;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] float timerToSpawn = 10f; //10f
    [SerializeField] int maxEnemies = 15;
    private float spawnVariation = 4f;

    int spawnCount = 0;
    bool canSpawn = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(ReduceDelay());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(0.5f); //7.5
        canSpawn = true;

        while(canSpawn && spawnCount < maxEnemies)
        {
            var pos = Random.Range(0, 4);
            //var pos = 0;

            var chosen = spawnPoints[pos].position;

            //new Vector3(Random.Range(-spawnVariation, spawnVariation), 0, Random.Range(-spawnVariation, spawnVariation));
            Vector3 randomOffset = Vector3.zero;
            if(chosen.x != 0) randomOffset = new Vector3(0, 0, Random.Range(-spawnVariation, spawnVariation));
            else if(chosen.z != 0) randomOffset = new Vector3(Random.Range(-spawnVariation, spawnVariation), 0, 0);


            Vector3 spawnPosition = spawnPoints[pos].position + randomOffset;

            var enemy = Instantiate(enemyPrefab.gameObject, spawnPosition, spawnPoints[pos].rotation);
            enemy.GetComponent<Enemy>().SetTarget(target);

            spawnCount++;        
            yield return new WaitForSeconds(timerToSpawn);
        }
        canSpawn = false;
    }

    IEnumerator ReduceDelay()
    {
        yield return new WaitForSeconds(65);

        while (canSpawn && timerToSpawn > 3 && spawnCount < maxEnemies)
        {
            timerToSpawn -= 1f;
            yield return new WaitForSeconds(60);
        }
    }
}