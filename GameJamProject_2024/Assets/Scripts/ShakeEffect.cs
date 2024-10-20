using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class ShakeEffect : MonoBehaviour
{
    bool calledCoroutine = false;

    bool dowhile = false;

    Vector3 originalScale = Vector3.zero;

    //Sequence mySequence = DOTween.Sequence();

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("enemy enter");
            StartCoroutine(ShakeObject());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            StopCoroutine(ShakeObject());

            transform.localScale = originalScale;
            dowhile = false;

            //transform.DOScale(originalScale, 0.15f);
        }
    }

    IEnumerator ShakeObject()
    {
        if(calledCoroutine) yield return null;

        calledCoroutine = true;
        dowhile = true;

        while(dowhile)
        {
            // mySequence.Append(transform.DOShakeScale(1.25f, 1.25f));
            transform.DOShakeScale(1.25f, 0.75f); //COMMENT: ou (1.25f, 0.75f)

            yield return new WaitForSeconds(0.3f);
        }

        calledCoroutine = false;
    }
}
