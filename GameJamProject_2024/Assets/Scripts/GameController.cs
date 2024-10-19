using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int material = 100;

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
}
