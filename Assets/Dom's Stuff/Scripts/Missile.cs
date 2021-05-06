using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] private bool Testing;

    [Header("Components")]
    [SerializeField] private ProjectileScriptableObjects ProjectileOBJ;
    private Rigidbody rigidbody;

    [Header("Targeting")]
    [SerializeField] private Transform Player;
    [SerializeField] private Transform Boss;
    private Transform Target;
    private FinalBoss FB;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        FB = Boss.GetComponent<FinalBoss>();

        if (!Testing)
        {
            Player = PlayerRefs.instance.Player;
        }
        Target = Player;
    }

    private void FixedUpdate()
    {
        Vector3 Direciton = Target.position - rigidbody.position;
        Direciton.Normalize();

        Vector3 RotateAmount = Vector3.Cross(Direciton, transform.up);

        rigidbody.angularVelocity = -RotateAmount * (ProjectileOBJ.ProjectileSpeed / 2);
        rigidbody.velocity = transform.up * ProjectileOBJ.ProjectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Particle Disc"))
        {
            Target = Boss;
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(ProjectileOBJ.DamageDealt);
            ObjectPooling.DeSpawn(gameObject);
            ObjectPooling.Spawn(ProjectileOBJ.DestroyParticle.gameObject, transform.position, Quaternion.identity);
        }
        else if (other.CompareTag("Boss"))
        {
            other.GetComponent<EnemyBehavior>().TakeDamage(1);
            ObjectPooling.DeSpawn(gameObject);
            ObjectPooling.Spawn(ProjectileOBJ.DestroyParticle.gameObject, transform.position, Quaternion.identity);
            FB.MissilesReturned += 1;
        }
        else
        {
            ObjectPooling.DeSpawn(gameObject);
            ObjectPooling.Spawn(ProjectileOBJ.DestroyParticle.gameObject, transform.position, Quaternion.identity);
        }
    }
}
