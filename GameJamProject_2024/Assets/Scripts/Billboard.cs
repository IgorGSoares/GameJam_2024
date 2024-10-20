using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Acessa a câmera principal da cena
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Faz com que a sprite esteja sempre olhando para a câmera
        transform.forward = mainCamera.transform.forward;
    }
}
