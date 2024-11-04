using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform target;

    [SerializeField] GameObject[] enemies;

    [SerializeField] float timerToSpawn = 10f; //10f
    [SerializeField] int maxEnemies = 15;
    private float spawnVariation = 4f;

    int spawnCount = 0;
    bool canSpawn = false;

    void Start()
    {
        enemies = new GameObject[3]
        {
            Resources.Load<GameObject>("Prefabs/Characters/Enemies/Enemy_1"),
            Resources.Load<GameObject>("Prefabs/Characters/Enemies/Enemy_2"),
            Resources.Load<GameObject>("Prefabs/Characters/Enemies/Enemy_3"),
        };
        StartCoroutine(SpawnEnemies());
        StartCoroutine(ReduceDelay());
    }

    IEnumerator SpawnEnemies()
    {
        //Debug.Log("call enemy spawner");
        yield return new WaitForSeconds(0.5f); //7.5
        canSpawn = true;

        //Debug.Log("spawncount: " + spawnCount);

        while(canSpawn && spawnCount < maxEnemies)
        {
            Debug.Log("enter in while");

            var pos = 0;
            var chosen = spawnPoints[pos].position;
            int randomEnemy = 0;

            if(spawnCount <= 3)
            {
                chosen = spawnPoints[pos].position;
            }
            // else if (spawnCount > 3 && spawnCount <= 6)
            // {
            //     pos = Random.Range(0, 2);
            //     chosen = spawnPoints[pos].position;
            // }
            else if (spawnCount > 3 && spawnCount <= 10){
                pos = Random.Range(0, 4);
                chosen = spawnPoints[pos].position;
                randomEnemy = Random.Range(0, 2);
            }
            else{
                pos = Random.Range(0, 4);
                chosen = spawnPoints[pos].position;
                randomEnemy = Random.Range(0, 3);
            }

            Vector3 randomOffset = Vector3.zero;
            if(chosen.x != 0) randomOffset = new Vector3(0, 0, Random.Range(-spawnVariation, spawnVariation));
            else if(chosen.z != 0) randomOffset = new Vector3(Random.Range(-spawnVariation, spawnVariation), 0, 0);

            Vector3 spawnPosition = spawnPoints[pos].position + randomOffset;

            GameObject enemy = Instantiate(enemies[randomEnemy], spawnPosition, spawnPoints[pos].rotation); //enemies[randomEnemy]

            //enemy.GetComponent<Enemy>().SetTarget(target);
            var enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.SetTarget(target);

            if(spawnCount <= 3) enemyScript.SetInitialSpeed(0.75f); //enemyScript.GetSpeed() + 

            spawnCount++;        
            yield return new WaitForSeconds(timerToSpawn);
        }

        //Debug.Log("out of while");

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

    private void PassingSpawn()
    {

    }
}