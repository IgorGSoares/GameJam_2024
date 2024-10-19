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
        if(health <= 0) gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            attacking = true;
            enemy = other.GetComponent<Enemy>();
            StartCoroutine(Attack());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            attacking = false;
            enemy = null;
            StopCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        while(attacking && this.enemy.health > 0)
        {
            this.enemy.health -= 3;

            yield return new WaitForSeconds(delay);
        }
    }
}
