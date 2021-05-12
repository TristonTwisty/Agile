using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointAttack : MonoBehaviour
{
    [SerializeField] private NewFinalBoss Fb;
    public enum State { BatCollider, ThunderStrikeCollider}
    public State ActiveState;

    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();

        collider.isTrigger = true;
        collider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(ActiveState == State.ThunderStrikeCollider)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponentInChildren<ParticleShield>().ShieldOn)
                {
                    Debug.Log("Player Protected");
                }
                else
                {
                    other.GetComponent<Player>().TakeDamage(Fb.ShieldAttackDamage);
                }
            }
        }
    }
    [Header("Bat AttaCK")]
    [SerializeField] private ParticleSystem BlockExplopsion;
    private void OnTriggerEnter(Collider other)
    {
        if(ActiveState == State.BatCollider)
        {
            if (other.CompareTag("Target"))
            {
                Debug.Log("block hit");
                Fb.EndAttack = true;
                Fb.EB.TakeDamage(5);
                BlockExplopsion.Play();
                Destroy(other);
            }
            else if (other.CompareTag("Player"))
            {
                Fb.EndAttack = true;
                other.GetComponent<Player>().TakeDamage(Fb.BatAttackDamage);
                other.GetComponent<Rigidbody>().AddExplosionForce(Fb.PushForce, transform.position, 10, 5, ForceMode.Impulse);
                //other.GetComponent<Rigidbody>().AddForce(Fb.transform.forward * Fb.PushForce, ForceMode.VelocityChange);
            }
        }
    }
}