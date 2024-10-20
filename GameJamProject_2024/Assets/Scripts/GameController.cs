using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Health")]
    [SerializeField] int health = 4;
    [SerializeField] GameObject top;
    [SerializeField] Color damageColor;
    [SerializeField] Color originalColor;
    [SerializeField] Material mat;


    [Space]
    [SerializeField] int material = 50;
    [SerializeField] TextMeshProUGUI moneyText;
    private int selectedGolem = -1;

    #region Singleton

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        mat.color = originalColor;

        GameEvents.OnEntityKilled.AddListener((int material) =>
        {
            OnKilled(material);
        });
    }

    #endregion

    void Start()
    {
        //moneyText.text = "Material = " + material.ToString();
        moneyText.text = material.ToString();
    }

    private void Update()
    {
        //SelectGolem();
        if(health <= 0) Time.timeScale = 0; 
    }

    public bool HasEnoughGold(int cost)
    {
        return material >= cost;
    }

    public void SpendGold(int amount)
    {
        material -= amount;
        moneyText.text = "Material = " + material.ToString();
        Debug.Log("Gold restante: " + material);
    }

    public void GainMoney(int amount)
    {
        material += amount;
        moneyText.text = "Material = " + material.ToString();
    }

    public int GetPlayerGold()
    {
        return material;
    }

    private void OnKilled(int material)
    {
        this.material += material;
    }

    // public int SelectGolem()
    // {
    //     if (Input.GetKey("1"))
    //     {
    //         selectedGolem = 0;
    //     }
    //     else if (Input.GetKey("2"))
    //     {
    //         selectedGolem = 1;
    //     }
    //     else if (Input.GetKey("3"))
    //     {
    //         selectedGolem = 2;
    //     }
    //     return selectedGolem;
    // }

    public void SelectGolem(int golem)
    {
        selectedGolem = golem;
    }

    public int GetSelectedGolem() => selectedGolem;

    public void EnemyReachTop()
    {
        health--;
        mat.color = damageColor;
        mat.DOColor(originalColor, 1.25f);
        top.transform.DOPunchScale(Vector3.one * 1.5f, 1.25f);
        //top.transform.DOPunchRotation(Vector3.up * 360, 1.25f);
    }
}
