using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAFterEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem effect;
   

    // Update is called once per frame
    void Update()
    {
        if (!effect.IsAlive())
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
