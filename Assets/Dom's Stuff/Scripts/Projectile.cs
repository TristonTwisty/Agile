using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public ProjectileScriptableObjects ProjectileOBJ;

    private float TimeAlive;

    private void OnEnable()
    {
        float width = Random.Range(-1, 1) * ProjectileOBJ.MaxSpread;
        float height = Random.Range(-1, 1) * ProjectileOBJ.MaxSpread;

        GetComponent<Rigidbody>().AddForce(transform.forward * ProjectileOBJ.ProjectileSpeed + transform.right * width + transform.up * height);

        TimeAlive = ProjectileOBJ.ProjectileLifetime;
    }

    private void Update()
    {
        TimeAlive -= Time.deltaTime;

        if(TimeAlive <= 0)
        {
            ObjectPooling.DeSpawn(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectPooling.Spawn(ProjectileOBJ.DestroyParticle.gameObject, transform.position, Quaternion.identity);
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(ProjectileOBJ.DamageDealt);
        }
        ObjectPooling.DeSpawn(gameObject);
    }
}