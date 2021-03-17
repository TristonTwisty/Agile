using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<RigidbodyFPS>().CanWallWalk = true;
        Destroy(gameObject);
    }
}
