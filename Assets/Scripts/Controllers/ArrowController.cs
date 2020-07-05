using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject arrow;

    Vector3 startPos, endPos, direction;
    float pullForce = 5f;

    private void Awake()
    {
        arrow = FindObjectOfType<Arrow>().gameObject;
        arrow.SetActive(false);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // startTouchTime = Time.time;
            arrow.SetActive(true);
            startPos = Input.GetTouch(0).position;
            arrow.transform.LookAt(startPos);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            //endTouchTime = Time.time;
            //timeInterval = endTouchTime - startTouchTime;

            endPos = Input.GetTouch(0).position;
            arrow.transform.LookAt(endPos);
            float distance = Vector3.Distance(startPos, endPos);
            arrow.transform.localScale = new Vector3(distance, distance, 0);
            direction = startPos - endPos;

           

        }
    }
}
