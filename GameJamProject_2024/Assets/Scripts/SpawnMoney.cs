using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnMoney : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] Money prefab;

    public void InstantiateMoney()
    {
        var m = Instantiate(prefab.gameObject, transform.position, transform.rotation);
        var money = m.GetComponent<Money>();
        money.value = this.value;
    }
}
