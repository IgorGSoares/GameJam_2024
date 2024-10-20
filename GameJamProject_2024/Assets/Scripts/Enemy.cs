using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] Transform target;

    //[SerializeField] SpawnMoney spawnMoney;
    private GameObject moneyPrefab;


    [SerializeField] float attackDelay;
    [SerializeField] float limitDistance = 2.5f;

    Golem golem;

    EnemyStates enemyStates = EnemyStates.Move;

    private Renderer myRenderer;
    private bool isGettingDamage = false;

    //Status
    public int health = 3;
    [SerializeField] int damage = 1;
    [SerializeField] int drop = 5;
    [SerializeField] float speed;
    

    public void SetTarget(Transform t) => target = t;


    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        moneyPrefab = Resources.Load<GameObject>("Prefabs/Prefab_Money");
    }

    void Update()
    {
        if(health <= 0)
        {
            gameObject.SetActive(false);
            //spawnMoney.InstantiateMoney(drop);

            GameObject moneyInstance = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
            moneyInstance.GetComponent<Money>().amount = drop;

            GameEvents.OnEntityKilled.Invoke(drop);
            //Object.Destroy(gameObject);
        }

        CheckGolemHealth();

        if (enemyStates == EnemyStates.Attack) return;

        if(golem != null && CheckDistance() <= limitDistance)
        {            
            enemyStates = EnemyStates.Attack;
            StartCoroutine(Attack());
            return;
        }

        if(gameObject.transform.position == target.position)
        {
            gameObject.SetActive(false);
            //Debug.Log("One enemy reached the top");
            GameController.Instance.EnemyReachTop();
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
            Debug.Log("golem defeated");
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
        while(enemyStates == EnemyStates.Attack && golem.health > 0)
        {
            Debug.Log("Attack");

            this.golem.health -= 3;
            this.golem.OnDamageTaken(damage);
            yield return new WaitForSeconds(attackDelay);
        }
        
        Debug.Log("exit while");
    }
}
