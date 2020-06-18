using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //not needed to be serialized, because ball gona be founded on first fram via update
    //used for debbuging
    [SerializeField] Transform ball = null;
    [SerializeField] AudioClip ballDeath = null;

    [Tooltip("Starting speed of the enemy")]
    [SerializeField] float speed = 10f;
    [Tooltip("AMount of speed added with each LVL")]
    [SerializeField] float speedIncrementor = 5f;

    AudioSource audioSource;
    private void Start()
    {
        GameHandler.OnReset += SpeedIncrease;// subscribing for game reset
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        FindBall();

        FollowBall();
    }

    private void FindBall()
    {
       //TODO cause error in console wich not affect gameplay=> on the frame the bass is destroyed but new on doesnt exists
        if (ball == null)
        {
            ball = FindObjectOfType<BallController>().transform;
        }
    }

    // following the ball by X axis with frame independent speed
    void FollowBall()
     {
        Vector3 targetPosition = new Vector3(ball.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
     }

    //destroys the Ball when collides with it 
    void OnCollisionEnter(Collision collision)
     {
        if (collision.gameObject.tag == "Ball")
        {
            // TODO add particle or sound effect
            audioSource.PlayOneShot(ballDeath, 0.4f);
            Destroy(collision.gameObject);
        }
     }
    //increasing speed with each lvl
    void SpeedIncrease()
    {
        if (speed <= 50)
        {
            speed += speedIncrementor;
        }
    }
}
