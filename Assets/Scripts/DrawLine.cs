using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] GameObject LinePref = null;
    [SerializeField] GameObject currentLine = null;
    LineRenderer lr;
    public List<Vector2> fingerPositions;

    Touch touch;
   

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            CreateLine();
        }
        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 temporaryPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (Vector2.Distance(temporaryPosition, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
            {
                UpdateLine(temporaryPosition);
            }
           
        }
        
    }
    void CreateLine()
    {
        currentLine = Instantiate(LinePref, Vector3.zero, Quaternion.identity);
        lr = currentLine.GetComponent<LineRenderer>();

        fingerPositions.Clear();

        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));

        lr.SetPosition(0, fingerPositions[0]);
        lr.SetPosition(1, fingerPositions[1]);
    }
    void UpdateLine(Vector2 newFingerPose)
    {
        fingerPositions.Add(newFingerPose);
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1,newFingerPose);
    }
}
