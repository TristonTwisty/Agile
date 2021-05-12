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
    [SerializeField] private Transform BossTarget;
    //[SerializeField] private FinalBoss FB;
    [SerializeField] private NewFinalBoss FB;
    private Transform Target;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        FB = GameObject.FindGameObjectWithTag("Boss").GetComponent<NewFinalBoss>();
        BossTarget = FB.BossTarget;

        if (!Testing)
        {
            Player = PlayerRefs.instance.Player;
        }
        Target = Player;
    }

    private void OnEnable()
    {
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
            Target = BossTarget;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectPooling.Spawn(ProjectileOBJ.DestroyParticle.gameObject, transform.position, Quaternion.identity);
        ObjectPooling.DeSpawn(gameObject);

        if (collision.transform.CompareTag("Boss"))
        {
            collision.transform.GetComponent<EnemyBehavior>().TakeDamage(1);
            FB.MissilesReturned += 1;
        }
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().TakeDamage(ProjectileOBJ.DamageDealt);
        }
    }
}
