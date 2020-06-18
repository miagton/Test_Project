using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [Header("GameObjects to track")]
    [SerializeField] GameObject ball = null;
    [SerializeField] GameObject[] targets = null;
    //[SerializeField] GameObject targetPrefab = null;
    //[SerializeField] Transform[] targetsPositions = null;

    //EVENT triggered when lvl completed
    public static event Action OnReset;

    //lvl, increments on game Restet
    int currentLVL = 0;
   
    GameObject currentBall;//tracks current existing ball
    Vector3 ballOriginPosition;//stores original position of the ball
    void Awake()
    {
       //storing references
        currentBall = FindObjectOfType<BallController>().gameObject;
        ballOriginPosition = currentBall.transform.position;

        OnReset += RespawnTargets;//subscribing to event
    }

    
    void Update()
    {
        DetectBall();
        DetectObjectives();
       
    }

    private void DetectObjectives()
    {
        int num = FindObjectsOfType<Target>().Length;
        if (num < 1)//checking if all targets are destroyed
        {
            currentLVL++;//incrementing lvl before reset
            if (OnReset != null)
            {
                OnReset();// invoking our event
            }
            RespawnTargets();// respawning our targets
        }
    }

    //TODO possible to add random positions and amount of targets to spawn
    public void RespawnTargets()// spawning prefabs at positions
    {
        foreach (var obj in targets)
        {
            obj.SetActive(true);
        }
    }

    private void DetectBall()
    {
        int num = FindObjectsOfType<BallController>().Length;
        if (num<1)// checking if there no balls in scene
        {
           // creating new ball on its origin position
            GameObject ballNew= Instantiate(ball, ballOriginPosition, Quaternion.identity);
            //tracking new ball
            currentBall = ballNew;

        }
    }
    public int GetLVL()// returns LvL value for Text Updater
    {
        
        return currentLVL;
    }

    
}
