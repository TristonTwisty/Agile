using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Destroys self after it's done playing

public class DestroyParticleSystem : MonoBehaviour
{
    private ParticleSystem PS;

    private void Start()
    {
        PS = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!PS.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
