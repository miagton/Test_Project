using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject targetPrefab = null;
    [SerializeField] GameObject ball = null;
    [SerializeField] Transform[] targetsPositions = null;

  //  public delegate void Reset();
    public static event Action OnReset;


     Transform[] destroyableObjects ;
    GameObject currentBall;
    Vector3 ballOriginPosition;
    void Awake()
    {
        currentBall = FindObjectOfType<BallController>().gameObject;
        ballOriginPosition = currentBall.transform.position;
        destroyableObjects = targetsPositions;

        OnReset += RespawnTargets;
    }

    
    void Update()
    {
        DetectBall();
        DetectObjectives();
       
    }

    private void DetectObjectives()
    {
        int num = FindObjectsOfType<Target>().Length;
        if (num < 1)
        {
            if (OnReset != null)
            {
                OnReset();
            }
            RespawnTargets();
        }
    }

    private void RespawnTargets()
    {
        foreach (var pos in destroyableObjects)
        {
            Instantiate(targetPrefab, pos.position, Quaternion.identity);
        }
    }

    private void DetectBall()
    {
        int num = FindObjectsOfType<BallController>().Length;
        if (num<1)
        {
           
            GameObject ballNew= Instantiate(ball, ballOriginPosition, Quaternion.identity);
            currentBall = ballNew;

        }
    }
}
