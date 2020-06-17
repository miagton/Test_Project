using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform ball;
    

    Rigidbody rb;
    LineRenderer lr;
    Touch _touch;
    Vector3 dragStartPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame

    void Update()
    {

        transform.LookAt(ball);
      //  transform.position = GetTouchPosition();

    }

    Vector3 GetTouchPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0; // 
        if (plane.Raycast(ray, out distance))
        {
            Vector3 draggingPos = ray.GetPoint(distance);
            return draggingPos;
        }
        else return Vector3.zero;
    }
   
}
