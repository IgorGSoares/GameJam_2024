using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Money : MonoBehaviour
{
    public int value = 0;

    private void OnMouseDown()
    {
        GameManager.Instance.money++;
        gameObject.SetActive(false);
    }
}
