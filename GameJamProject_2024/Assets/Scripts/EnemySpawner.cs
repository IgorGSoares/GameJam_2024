using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform target;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] float timerToSpawn = 10;

    int spawnCount = 0;

    bool canSpawn = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(ReduceDelay());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(7.5f);
        canSpawn = true;

        while(canSpawn)
        {
            var pos = Random.Range(0, 4);
            var enemy = Instantiate(enemyPrefab.gameObject, spawnPoints[pos].position, spawnPoints[pos].rotation);
            enemy.transform.position = spawnPoints[pos].position;
            enemy.GetComponent<Enemy>().SetTarget(target);

            yield return new WaitForSeconds(timerToSpawn);
        }
    }

    IEnumerator ReduceDelay()
    {
        yield return new WaitForSeconds(65);

        while(canSpawn && (timerToSpawn > 3 && spawnCount == 15))
        {
            timerToSpawn -= 1f;
            spawnCount++;
            if(spawnCount >= 15) canSpawn = false;

            yield return new WaitForSeconds(60);
        }

        canSpawn = false;
    }
}
