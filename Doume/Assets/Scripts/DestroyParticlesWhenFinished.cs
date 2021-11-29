using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesWhenFinished : MonoBehaviour
{
    private ParticleSystem particle;
    private bool hasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        hasPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (particle.isEmitting)
        {
            hasPlayed = true;
        }
        else if (hasPlayed == true && particle.isEmitting == false)
        {
            Destroy(gameObject);
        }
    }
}
