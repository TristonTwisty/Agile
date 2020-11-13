using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileScriptableObjects ProjectileOBJ;

    private void Update()
    {
        float Distance = ProjectileOBJ.ProjectileSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward.normalized * Distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(ProjectileOBJ.DamageDealt);
        }
    }
}