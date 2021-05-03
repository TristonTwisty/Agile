using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [Header("Testing")]
    private bool Testing;

    [Header("Components")]
    [SerializeField] private ProjectileScriptableObjects ProjectileOBJ;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (!Testing)
        {
            Player = PlayerRefs.instance.Player;
        }
    }

    private void FixedUpdate()
    {
        Vector3 Direciton = Player.position - rigidbody.position;
        Direciton.Normalize();

        Vector3 RotateAmount = Vector3.Cross(Direciton, transform.up);

        rigidbody.angularVelocity = -RotateAmount * (ProjectileOBJ.ProjectileSpeed / 2);
        rigidbody.velocity = transform.up * ProjectileOBJ.ProjectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectPooling.Spawn(ProjectileOBJ.DestroyParticle.gameObject, transform.position, Quaternion.identity);
        ObjectPooling.DeSpawn(gameObject);

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(ProjectileOBJ.DamageDealt);
        }
    }
}
