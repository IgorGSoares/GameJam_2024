using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public int health = 6;
    [SerializeField] Enemy curEnemy;
    [SerializeField] float delay;

    [SerializeField] List<Enemy> enemyList = new List<Enemy>();

    bool attacking = false;

    void Update()
    {
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(health <= 0) gameObject.SetActive(false);

        CheckEnemyHealth();

        if(curEnemy != null && CheckDistance() <= 4)
        {
            attacking = true;
            StartCoroutine(Attack());
            return;
        }

    }

    private float CheckDistance()
    {
        if(curEnemy == null) return -1;

        var pos = transform.position - curEnemy.transform.position;
        var distance = pos.magnitude;
        // Debug.Log(distance);
        return distance;
    }

    // private float CheckDistanceNewEnemy(Transform t)
    // {
    //     var pos = transform.position - t.transform.position;
    //     var distance = pos.magnitude;
    //     return distance;
    // }

    private void CheckEnemyHealth()
    {
        if(curEnemy == null) return;

        if(curEnemy.health <= 0)
        {
            Debug.Log("Enemy killed");
            enemyList.RemoveAt(0);
            if(enemyList.Count == 0)
            {
                curEnemy = null;
                StopCoroutine(Attack());
            }
            else curEnemy = enemyList[0];
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            var enemy = other.GetComponent<Enemy>();
            if(curEnemy == null)
            {
                curEnemy = enemy;
                enemyList.Add(curEnemy);
            }
            else
            {
                enemyList.Add(enemy);
                // if(CheckDistance() > CheckDistanceNewEnemy(other.transform))
                // {
                // }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            attacking = false;
            curEnemy = null;
            StopCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        while(attacking && (this.curEnemy != null && this.curEnemy.health > 0))
        {
            Debug.Log("Attaking enemy");
            this.curEnemy.health -= 3;

            yield return new WaitForSeconds(delay);
        }
    }
}
