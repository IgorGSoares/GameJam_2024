using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        
        CheckGolemHealth();

        if (enemyStates == EnemyStates.Attack) return;

        if(golem != null && CheckDistance() <= 2.5f)
        {            
            enemyStates = EnemyStates.Attack;
            StartCoroutine(Attack());
            return;
        }

        gameObject.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private float CheckDistance()
    {
        if(golem == null) return -1;

        var pos = transform.position - golem.transform.position;
        var distance = pos.magnitude;
        // Debug.Log(distance);
        return distance;
    }

    private void CheckGolemHealth()
    {
        if(golem == null) return;

        if(golem.health <= 0)
        {
            Debug.Log("enter low health");
            golem = null;
            enemyStates = EnemyStates.Move;
            StopCoroutine(Attack());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Golem")
        {
            golem = other.GetComponent<Golem>();
            //Debug.Log(golem);
            //enemyStates = EnemyStates.Attack;
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.tag == "Golem")
    //     {
    //         Debug.Log("exit trigger");
    //         golem = null;
    //         enemyStates = EnemyStates.Move;
    //         StopCoroutine(Attack());
    //     }
    // }

    IEnumerator Attack()
    {
        while(enemyStates == EnemyStates.Attack && golem.health > 0)
        {
            Debug.Log("Attack");

            this.golem.health -= 3;
            yield return new WaitForSeconds(attackDelay);
        }
        
        Debug.Log("exit while");
    }
}
