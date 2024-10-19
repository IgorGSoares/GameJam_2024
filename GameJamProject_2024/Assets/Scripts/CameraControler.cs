using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class CameraControler : MonoBehaviour
{
    int direction = 0;
    //bool pressed = false;

    [SerializeField] Transform[] positions;
    [SerializeField] int currentIndex = 0;
    //[SerializeField] Transform target;

    [SerializeField] float duration = 2f;

    [SerializeField] AnimationCurve curve;


    private bool isMoving = false;
    private float moveSpeed = 30f;

    void Start()
    {
        //Time.timeScale = 0;
        transform.position = positions[currentIndex].position;
        //Camera.main.transform.LookAt(target);
       // Camera.main.transform.parent = positions[currentIndex];
    }

    void Update()
    {
        //pressed = false;

        // Camera.main.transform.position = Vector3.zero;
        transform.position = positions[currentIndex].position;

        if (Input.GetKeyDown(KeyCode.A) && !isMoving) { direction = 1; ChangeCamera(); }
        if (Input.GetKeyDown(KeyCode.D) && !isMoving) { direction = -1; ChangeCamera();}

    }

    private void ChangeCamera()
    {
        if (currentIndex + direction > positions.Length - 1) currentIndex = 0;
        else if (currentIndex + direction < 0) currentIndex = 3;
        else currentIndex += direction;

        StartCoroutine(MoveToPosition(positions[currentIndex].position));

        // Camera.main.transform.position = positions[currentIndex].position;
        // Camera.main.transform.rotation = positions[currentIndex].rotation;
        // //Camera.main.transform.LookAt(target);
        // Camera.main.transform.parent = positions[currentIndex];
        
    }

    IEnumerator MoveToPosition(Vector3 targetPos)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 startRotation = transform.eulerAngles;

        float progress = 0f;
       // float duration = Vector3.Distance(startPos, targetPos) / moveSpeed;

        //Camera.main.transform.DORotate(Vector3.up * direction * 90, duration);

        while (progress < 1f)
        {
            progress += Time.unscaledDeltaTime / duration;

            transform.position = Vector3.Lerp(startPos, targetPos, curve.Evaluate(progress));
            transform.eulerAngles = Vector3.Lerp(startRotation, startRotation + new Vector3(0, direction == 1 ? 90 : -90,0), progress);

            //Camera.main.transform.LookAt(target);
            //yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            yield return null;
        }

        transform.position = targetPos;
        transform.eulerAngles = Vector3.Lerp(startRotation, startRotation + new Vector3(0,direction == 1 ? 90 : -90,0), progress);

        //Camera.main.transform.parent = positions[currentIndex];

        isMoving = false;
    }
}
