using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallController : MonoBehaviour
{
    [SerializeField] float forceApplied = 100f;
    [SerializeField] float maxDrag = 5f;
    [SerializeField] Sprite arrow;
    

    Rigidbody rb;
    LineRenderer lr;
    Touch _touch;
    Vector3 dragStartPos;
   // Vector3 startPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        //  Plane plane = new Plane(Vector3.up, transform.position);
        //startPosition = transform.position;
       
        GameHandler.OnReset += ResetBall;
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
    void StartDrag()//proccesing start of the dragging
    {

            Vector3 dragStartPos = GetRayHitPosition();
            
            lr.positionCount = 1;
            lr.SetPosition(0, dragStartPos);
    }

    void Dragging()//dragging in progress
    {

            Vector3 draggingPos = GetRayHitPosition();
          
                          
                     
            lr.positionCount = 2;
            lr.SetPosition(0, draggingPos);
    }

    void DragEnding()//dragging finished
    {
        lr.positionCount = 0;
        Vector3 dragEnd= GetRayHitPosition();

        Vector3 force = dragStartPos - dragEnd;//direction
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * forceApplied;

        rb.AddForce(clampedForce, ForceMode.Impulse);
    }

    private Vector3 GetRayHitPosition()//registering position wich ray has hit
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance ; // 
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            hitPoint.y = 0.5f;
            return hitPoint;
        }
        else return Vector3.zero;
    }
   public void ResetBall()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        GameHandler.OnReset -= ResetBall;
    }
}