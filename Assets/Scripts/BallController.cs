using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] float forceApplied = 100f;
    [SerializeField] float maxDrag = 5f;


    Rigidbody rb;
    LineRenderer lr;
    Touch _touch;
    Vector3 dragStartPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        //  Plane plane = new Plane(Vector3.up, transform.position);
    }

    
    void Update()
    {
       
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                StartDrag();
            }
            if (_touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (_touch.phase == TouchPhase.Ended)
            {
                DragEnding();
            }
        }
    }
    void StartDrag()
    {


            Vector3 dragStartPos = GetRayHitPosition();
            
            lr.positionCount = 1;
            lr.SetPosition(0, dragStartPos);
    }

    void Dragging()
    {

            Vector3 draggingPos = GetRayHitPosition();
          
                          
                     
            lr.positionCount = 2;
            lr.SetPosition(0, draggingPos);
    }

    void DragEnding()
    {
        lr.positionCount = 0;
        Vector3 dragEnd= GetRayHitPosition();

        Vector3 force = dragStartPos - dragEnd;//direction
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * forceApplied;

        rb.AddForce(clampedForce, ForceMode.Impulse);
    }

    private Vector3 GetRayHitPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0; // 
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            hitPoint.y = 0.5f;
            return hitPoint;
        }
        else return Vector3.zero;
    }
}