using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forceApplied = 100f;
    [SerializeField] GameObject ball = null;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        ApplyVelocity(); 
        ContolLookAt();
    }

    private void ApplyVelocity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            RaycastHit raycastHit;
            if(Physics.Raycast(ray, out raycastHit))
            {
                var rigidBody = raycastHit.collider.GetComponent<Rigidbody>();
                if (rigidBody != null)
                {
                    rigidBody.AddForceAtPosition(ray.direction*forceApplied, raycastHit.point, ForceMode.VelocityChange);
                    Gizmos.DrawLine(transform.position, raycastHit.point);
                }
            }
        }
    }

    private void ContolLookAt()
    {
        transform.LookAt(ball.transform);
    }
   void ApplyForce()
    {
        var ray2 = Physics.Raycast(transform.position, ball.transform.position);
        RaycastHit raycastHit;

    }
}
