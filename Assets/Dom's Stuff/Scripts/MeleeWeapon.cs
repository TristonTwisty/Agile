using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] GameObject Owner;
    [SerializeField] private MeleeScriptableObject MeleeOBJ;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Collider>().enabled = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), Owner.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Rigidbody>().AddForce(Owner.transform.forward * MeleeOBJ.KnockbackPower, ForceMode.Impulse);
            other.GetComponent<EnemyBehavior>().TakeDamage(MeleeOBJ.DamageDealt);
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().AddForce(Owner.transform.forward * MeleeOBJ.KnockbackPower, ForceMode.Impulse);
        }
        else
        {   
            Debug.Log("Wrong tag buddy");
        }
    }
}