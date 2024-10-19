using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    int direction = 0;
    //bool pressed = false;

    [SerializeField] Transform[] positions;
    [SerializeField] int currentIndex = 0;
    [SerializeField] Transform target;

    void Start()
    {
        Camera.main.transform.position = positions[currentIndex].position;
        Camera.main.transform.LookAt(target);
        Camera.main.transform.parent = positions[currentIndex];
    }

    void Update()
    {
        //pressed = false;

        // Camera.main.transform.position = Vector3.zero;
        Camera.main.transform.position = positions[currentIndex].position;

        if (Input.GetKeyDown(KeyCode.A)) { direction = 1; ChangeCamera(); }
        if (Input.GetKeyDown(KeyCode.D)) { direction = -1; ChangeCamera();}

        // //var direction = Input.GetAxis("Horizontal") * 10f * Time.deltaTime;
        // // Camera.main.transform.Translate(direction, posCam.y, posCam.z);

        // var posCam = Camera.main.transform.position;
        // if(pressed)
        // {
        //     Camera.main.transform.position = new Vector3(posCam)
        // }

        // if(pressed)
        // {
        //     if(currentIndex + direction > positions.Length - 1) currentIndex = 0;
        //     else if(currentIndex + direction < 0) currentIndex = 3;
        //     else currentIndex += direction;

        //     Camera.main.transform.parent = positions[currentIndex];
        //     Camera.main.transform.position = new Vector3(0,0,0);
        // }
    }

    // [ContextMenu("Change camera zero")]
    // private void ChangeCameraZero() => Camera.main.transform.position = Vector3.zero;
    private void ChangeCamera()
    {
        if (currentIndex + direction > positions.Length - 1) currentIndex = 0;
        else if (currentIndex + direction < 0) currentIndex = 3;
        else currentIndex += direction;

        Camera.main.transform.position = positions[currentIndex].position;
        Camera.main.transform.LookAt(target);
        Camera.main.transform.parent = positions[currentIndex];
        
    }
}
