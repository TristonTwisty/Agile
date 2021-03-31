using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystemDamage : MonoBehaviour
{
    [SerializeField] private float DamageDealt;

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(DamageDealt);
        }

        if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehavior>().TakeDamage(DamageDealt);
        }
    }

    private void OnParticleTrigger()
    {

    }
}
