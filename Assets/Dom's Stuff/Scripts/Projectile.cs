using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public ProjectileScriptableObjects ProjectileOBJ;

    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * ProjectileOBJ.ProjectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectPooling.DeSpawn(gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<PlayerHealth>().TakeDamage(ProjectileOBJ.DamageDealt);
        }
    }
}