using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceByPull : MonoBehaviour
{
    Vector3 startPos, endPos, direction;
    [Range(0, 6)]
    [SerializeField] float pullForce = 5f;

    //float startTouchTime,endTouchTime,timeInterval;
    

    
    void FixedUpdate()
    {
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
           // startTouchTime = Time.time;
            startPos = Input.GetTouch(0).position;
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //endTouchTime = Time.time;
            //timeInterval = endTouchTime - startTouchTime;

            endPos = Input.GetTouch(0).position;

            direction = startPos - endPos;

            GetComponent<Rigidbody>().AddForce(-direction * pullForce);


        }
    }
}
