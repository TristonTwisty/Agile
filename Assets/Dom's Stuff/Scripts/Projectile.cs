using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileScriptableObjects ProjectileOBJ;
    private Rigidbody RB = null;
    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.AddForce(transform.forward * ProjectileOBJ.ProjectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}