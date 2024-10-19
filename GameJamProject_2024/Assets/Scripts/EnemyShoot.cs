using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public int health = 6;
    [SerializeField] Transform target;
    [SerializeField] float speed;

    [SerializeField] float attackDelay;
    [SerializeField] float limitDistance = 2.5f;

    [SerializeField] Projectile_Enemy bullet;

    Golem golem;

    EnemyStates enemyStates = EnemyStates.Move;
    

    void Update()
    {
        if(health <= 0) gameObject.SetActive(false);
        
        CheckGolemHealth();

        if (enemyStates == EnemyStates.Attack) return;

        if(golem != null && CheckDistance() <= limitDistance)
        {            
            //Debug.Log(CheckDistance());

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

    IEnumerator Attack()//TODO: atirar aqui
    {
        while(enemyStates == EnemyStates.Attack && golem.health > 0)
        {
            Debug.Log("Attack");

            var projectile = Instantiate(bullet, transform.position, transform.rotation);
            projectile.GetComponent<Projectile_Enemy>().target = golem;

            Debug.Log(projectile.GetComponent<Projectile_Enemy>().target);

            yield return new WaitForSeconds(attackDelay);
        }
        
        Debug.Log("exit while");
    }
}
