using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Spawner : MonoBehaviour
{
    private GameObject golem;

    void Start()
    {
        golem = Resources.Load<GameObject>("Prefabs/Golem");
    }

    private void OnMouseDown()
    {
        Vector3 offset = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Instantiate(golem, offset, Quaternion.identity);
    }
}
