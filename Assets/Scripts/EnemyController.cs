using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform ball = null;
    [SerializeField] float speed = 10f;

    private void Start()
    {
        GameHandler.OnReset += SpeedIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        ball = FindObjectOfType<BallController>().transform;
        FollowBall();
    }

     void FollowBall()
     {
        Vector3 targetPosition = new Vector3(ball.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
     }

     void OnCollisionEnter(Collision collision)
     {
        if (collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject);
        }
     }
    void SpeedIncrease()
    {
        if (speed <= 50)
        {
            speed += 5;
        }
    }
}
