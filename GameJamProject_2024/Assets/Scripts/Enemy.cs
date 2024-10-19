using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 6;
    [SerializeField] Transform target;
    [SerializeField] float speed;

    void Update()
    {
        if(health <= 0) gameObject.SetActive(false);
        gameObject.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
