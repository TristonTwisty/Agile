﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<CharacterControllerFPS>().CanWallWalk = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<CharacterControllerFPS>().CanWallWalk = true;
        Destroy(gameObject);
    }
}
