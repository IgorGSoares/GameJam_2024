using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public int health = 6;
    [SerializeField] Enemy enemy;
    [SerializeField] float delay;

    bool attacking = false;

    void Update()
    {
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            StartCoroutine(Attack());
            enemy = other.GetComponent<Enemy>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            StopCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Attack");
        this.enemy.health -= 3;
    }
}
