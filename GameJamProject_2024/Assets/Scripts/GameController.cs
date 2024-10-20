using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int material = 100;
    private int selectedGolem = 0;

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

        GameEvents.OnEntityKilled.AddListener((int material) =>
        {
            OnKilled(material);
        });
    }

    #endregion

    // private void Update()
    // {
    //     SelectGolem();    
    // }

    public bool HasEnoughGold(int cost)
    {
        return material >= cost;
    }

    public void SpendGold(int amount)
    {
        material -= amount;
        Debug.Log("Gold restante: " + material);
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
}
