using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Acessa a c�mera principal da cena
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Faz com que a sprite esteja sempre olhando para a c�mera
        transform.forward = Camera.main.transform.forward;
    }
}
