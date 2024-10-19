using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Spawner : MonoBehaviour
{
    private GameObject[] golems;

    private Color initialColor;
    private Renderer myRenderer;

    void Start()
    {
        golems = new GameObject[3]
        {
            Resources.Load<GameObject>("Prefabs/Characters/Golens/Golem_1"),
            Resources.Load<GameObject>("Prefabs/Characters/Golens/Golem_2"),
            Resources.Load<GameObject>("Prefabs/Characters/Golens/Golem_3"),
        };
        myRenderer = GetComponent<Renderer>();
        initialColor = myRenderer.material.color;
    }

    private void OnMouseDown()
    {
        int currentGolem = GameController.Instance.SelectGolem();

        if (GameController.Instance.HasEnoughGold(golems[currentGolem].GetComponent<Golem>().cost))
        {
            Vector3 offset = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(golems[currentGolem], offset, Quaternion.identity);

            GameController.Instance.SpendGold(golems[currentGolem].GetComponent<Golem>().cost);
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
