using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Money : MonoBehaviour
{
    [SerializeField] float timer = 10f;
    public int amount = 0;

    //private bool isBeingCollected = false;

    void Start()
    {
        Debug.Log("instantiated money");
        StartCoroutine(Disapear());
    }

    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(timer);
        
        transform.DOScale(0.1f, 0.25f).SetEase(Ease.InBounce).OnComplete(() =>{
            transform.DOKill();
            gameObject.SetActive(false);
        });
    }

    private void OnMouseDown()
    {
        //isBeingCollected = true;
        Debug.Log("click money");
        StopCoroutine(Disapear());
        GameController.Instance.GainMoney(amount);
        //GameManager.Instance.money++;
        gameObject.SetActive(false);
    }
}
