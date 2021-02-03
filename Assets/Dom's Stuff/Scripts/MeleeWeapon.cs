using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] GameObject Owner;
    [SerializeField] private MeleeScriptableObject MeleeOBJ = null;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Collider>().enabled = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), Owner.GetComponent<Collider>());
        gameObject.layer = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shooter Enemy"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * MeleeOBJ.KnockbackPower, ForceMode.Impulse);
            other.GetComponent<ShootingAI>().TakeDamage(MeleeOBJ.DamageDealt);
        }

        else if (other.CompareTag("Melee Enemy"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * MeleeOBJ.KnockbackPower, ForceMode.Impulse);
            other.GetComponent<MeleeAI>().TakeDamage(MeleeOBJ.DamageDealt);
        }

        else if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * MeleeOBJ.KnockbackPower, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Wrong tag buddy");
        }
    }
}