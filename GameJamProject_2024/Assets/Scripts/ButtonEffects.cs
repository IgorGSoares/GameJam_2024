using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonEffects : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] Outline[] outlines;

    public void PressButton(int index)
    {
        buttons[index].transform.DOShakeScale(0.5f, 0.5f);

        if(outlines.Length != 0)
        {
            foreach (var outline in outlines)
            {
                outline.enabled = false;
            }
            outlines[index].enabled = true;
        }
    }
}
