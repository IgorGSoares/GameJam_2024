using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Spawner : MonoBehaviour
{
    [SerializeField] GameObject golem;
    private Color initialColor;
    private Renderer myRenderer;

    void Start()
    {
        golem = Resources.Load<GameObject>("Prefabs/Golem");
        myRenderer = GetComponent<Renderer>();
        initialColor = myRenderer.material.color;
    }

    private void OnMouseDown()
    {
        Vector3 offset = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Instantiate(golem, offset, Quaternion.identity);
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
