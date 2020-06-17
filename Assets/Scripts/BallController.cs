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
    Touch touch;
    Vector3 dragStartPos;
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
   
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                StartDrag();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                StopDrag();
            }
        }
        
       
    }

   void StartDrag()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);//Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.y = 2f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }
    void Dragging()
    {
        Vector3 draggingPos= Camera.main.ScreenToWorldPoint(touch.position);
        //dragStartPos.z/y/x=0;
        
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }
    void StopDrag()
    {
        lr.positionCount = 0;
        Vector3 dragEnd= Camera.main.ScreenToWorldPoint(touch.position);
        //dragEnd.x=0;
        Vector3 force = dragStartPos - dragEnd;//direction
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * forceApplied;

        rb.AddForce(clampedForce, ForceMode.Impulse);

    }
}
