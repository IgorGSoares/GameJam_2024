using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Enemy : MonoBehaviour
{
    public Golem target;
    [SerializeField] float limitDistance = 0.5f;
    [SerializeField] float speed;
    [SerializeField] float lifetime;

    void Update()
    {
        if(target != null && CheckDistanceToTarget() <= limitDistance)
        {
            target.health -= 3;
            target = null;
            gameObject.SetActive(false);
        }

        Debug.Log(target);

        if(!gameObject.activeSelf) return;

        //REMINDME: projetil ir para o infinito quando não houver mais alvo, ou se desativar após um tempo
        gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private float CheckDistanceToTarget()
    {
        if(target == null) return -1;

        var pos = transform.position - target.transform.position;
        var distance = pos.magnitude;
        // Debug.Log(distance);
        return distance;
    }
}
