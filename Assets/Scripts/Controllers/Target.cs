using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //particle effect for colliding moment
    [SerializeField] GameObject blowEffect = null;
    [SerializeField] AudioClip clip = null;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            audioSource.PlayOneShot(clip, 1f);
            if (blowEffect != null)// protecting of null reference
            {
                
                Instantiate(blowEffect, transform.position, Quaternion.Euler(-90f,0f,0f));
            }
           
            Destroy(collision.gameObject);//destroying ball
            Destroy(this.gameObject);//destroying this object
        }

    }
}
