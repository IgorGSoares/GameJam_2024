using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Golem : MonoBehaviour
{  
    [SerializeField] Enemy curEnemy;
    [SerializeField] float delay;

    [SerializeField] List<Enemy> enemyList = new List<Enemy>();

    bool attacking = false;
    bool calledCoroutine = false;

    private bool isGettingDamage = false;
    private Renderer myRenderer;

    //Status
    public int health = 5;
    public int damage = 1;
    public int cost = 10;
    public int drop = 7;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (health <= 0)
        {
            gameObject.SetActive(false);
            GameEvents.OnEntityKilled.Invoke(drop);
            //Object.Destroy(gameObject);
        }

        CheckEnemyHealth();

        if(curEnemy != null && CheckDistance() <= 4)
        {
            attacking = true;
            if(!calledCoroutine) StartCoroutine(Attack());
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
            calledCoroutine = false;
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
            calledCoroutine = false;
        }
    }

    // APENAS PARA FINS DE TESTE. REMOVER //////////////////////////////////////////////////////////////////////////
    // private void OnMouseDown()
    // {
    //     OnDamageTaken(1);
    // }

    public void OnDamageTaken(int damage)
    {
        health -= damage;

        if (!isGettingDamage)
        {
            StartCoroutine(ChangeAlphaChannel());
        }
    }

    IEnumerator ChangeAlphaChannel()
    {
        Debug.Log("GOLEM GET DAMAGED");
        
        isGettingDamage = true;

        float progress = 0f;
        float duration = 0.7f;
        float blinkSpeed = 0.2f;

        UnityEngine.Color originalColor = myRenderer.material.color;

        while (progress < duration)
        {
            progress += Time.deltaTime / duration;

            float alpha = Mathf.PingPong(progress / blinkSpeed, 0.5f);

            myRenderer.material.color = new UnityEngine.Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        myRenderer.material.color = originalColor;

        isGettingDamage = false;
    }

    IEnumerator Attack()
    {
        if(calledCoroutine) yield return null;

        calledCoroutine = true;

        while(attacking && (this.curEnemy != null && this.curEnemy.health > 0))
        {
            Debug.Log("Golem attaking enemy");
            //this.curEnemy.health -= 3;
            this.curEnemy.OnDamageTaken(damage);



            yield return new WaitForSeconds(delay);
        }
    }
}
