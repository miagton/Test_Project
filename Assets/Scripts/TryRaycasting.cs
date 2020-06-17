using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryRaycasting : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        /* if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
         {
             // create ray from the camera and passing through the touch position:
             Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
             // create a logical plane at this object's position
             // and perpendicular to world Y:
             Plane plane = new Plane(Vector3.up, transform.position);
             float distance = 0; // this will return the distance from the camera
             if (plane.Raycast(ray, out distance))
             { // if plane hit...
                 Vector3 pos = ray.GetPoint(distance); // get the point
                                                       // pos has the position in the plane you've touched
             }
         }*/
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

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0; // 
        if (plane.Raycast(ray, out distance))
        {
            Vector3 dragStartPos = ray.GetPoint(distance);
            dragStartPos.y = 0.5f;
            lr.positionCount = 1;
            lr.SetPosition(0, dragStartPos);
        }
    }

    void Dragging()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0; // 
        if (plane.Raycast(ray, out distance))
        {
            Vector3 draggingPos = ray.GetPoint(distance);
            draggingPos.y = 0.5f;
           

            float dist = Vector3.Distance(dragStartPos, draggingPos);
            
            if ( dist > maxDrag)
            {
                
                return;
            }
            lr.positionCount = 2;
            lr.SetPosition(0, draggingPos);
        }
    }

    void DragEnding()
    {
        lr.positionCount = 0;
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0; // 
        if (plane.Raycast(ray, out distance))
        {
            Vector3 dragEnd = ray.GetPoint(distance);
            Vector3 force = dragStartPos - dragEnd;//direction
            
           // float lineLenght = Mathf.Clamp(Vector3.Distance(dragStartPos, dragEnd), 0, maxDrag);
           // dragEnd = dragStartPos + (force.normalized * lineLenght);
            Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * forceApplied;

            rb.AddForce(clampedForce, ForceMode.Impulse);
        }
    }
}


