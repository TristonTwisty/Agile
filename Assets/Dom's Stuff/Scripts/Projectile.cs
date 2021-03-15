using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public ProjectileScriptableObjects ProjectileOBJ;

    private float TimeAlive;

    private void OnEnable()
    {
        //Vector3 Direction = transform.forward + new Vector3(Random.Range(-ProjectileOBJ.MaxSpread, ProjectileOBJ.MaxSpread), Random.Range(-ProjectileOBJ.MaxSpread, ProjectileOBJ.MaxSpread), Random.Range(-ProjectileOBJ.MaxSpread, ProjectileOBJ.MaxSpread));
        //GetComponent<Rigidbody>().AddForce(Direction * ProjectileOBJ.ProjectileSpeed);

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

    private void OnTriggerEnter(Collider other)
    {
        ObjectPooling.DeSpawn(gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(ProjectileOBJ.DamageDealt);
        }
    }
}