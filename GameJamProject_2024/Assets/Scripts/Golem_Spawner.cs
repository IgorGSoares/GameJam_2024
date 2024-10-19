using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Spawner : MonoBehaviour
{
    private GameObject golem;
    private Color initialColor;
    private Renderer myRenderer;

    void Start()
    {
        golem = Resources.Load<GameObject>("Prefabs/Characters/Golens/Golem");
        myRenderer = GetComponent<Renderer>();
        initialColor = myRenderer.material.color;
    }

    private void OnMouseDown()
    {
        if (GameController.Instance.HasEnoughGold(golem.GetComponent<Golem>().cost))
        {
            Vector3 offset = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(golem, offset, Quaternion.identity);

            GameController.Instance.SpendGold(golem.GetComponent<Golem>().cost);
            Debug.Log(GameController.Instance.GetPlayerGold());
        }
    }

    private void OnMouseOver()
    {
        myRenderer.material.SetColor("_Color", Color.green);
    }

    private void OnMouseExit()
    {
        myRenderer.material.SetColor("_Color", initialColor);
    }
}
