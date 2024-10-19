using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 6;
    [SerializeField] Transform target;
    [SerializeField] float speed;

    [SerializeField] float attackDelay;

    Golem golem;

    EnemyStates enemyStates = EnemyStates.Move;
    

    void Update()
    {
        if(health <= 0) gameObject.SetActive(false);
        if (enemyStates == EnemyStates.Attack) return;

        gameObject.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == "Golem")
    //     {
    //         golem = GetComponent<Golem>();
    //         enemyStates = EnemyStates.Attack;
    //     }
    // }
}
