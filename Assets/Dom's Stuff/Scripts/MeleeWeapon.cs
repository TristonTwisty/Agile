using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private bool BatTest;

    [Header("Weapon and Owner")]
    [SerializeField] Transform Owner;
    [SerializeField] private MeleeScriptableObject MeleeOBJ;

    [Header("Player")]
    [SerializeField] private Transform Player;

    [Header("Which character controller?")]
    [SerializeField] private bool HasCharacterController;
    [SerializeField] private bool HasRigibody;

    private void Start()
    {
        if (!BatTest)
        {
            Player = PlayerRefs.instance.Player.transform;
        }

        GetComponent<Collider>().isTrigger = true;
        GetComponent<Collider>().enabled = false;

        Physics.IgnoreCollision(GetComponent<Collider>(), Owner.GetComponent<Collider>());

        if (Player.GetComponent<CharacterController>() != null)
        {
            HasCharacterController = true;
            HasRigibody = false;
        }
        else if (Player.GetComponent<Rigidbody>() != null)
        {
            HasRigibody = true;
            HasCharacterController = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform != Owner)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit an enemy");
                other.GetComponent<Rigidbody>().AddForce(Owner.transform.forward * MeleeOBJ.RBKnockbackPower, ForceMode.Impulse);
                other.GetComponent<EnemyBehavior>().TakeDamage(MeleeOBJ.DamageDealt);
            }
            else if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().TakeDamage(MeleeOBJ.DamageDealt);

                if (HasCharacterController)
                {
                    other.GetComponent<CharacterController>().Move(Owner.forward * MeleeOBJ.CCKnockbackPower);
                }
                else if (HasRigibody)
                {
                    other.GetComponent<Rigidbody>().AddForce(Owner.forward * MeleeOBJ.RBKnockbackPower, ForceMode.Impulse);
                }
            }
            else if (other.CompareTag("Boss"))
            {
                other.GetComponent<EnemyBehavior>().TakeDamage(MeleeOBJ.DamageDealt);
                Owner.GetComponent<Rigidbody>().AddForce(-Owner.forward * 62, ForceMode.VelocityChange);
            }
            else if (other.CompareTag("Target"))
            {
                if (other.GetComponent<Rigidbody>())
                {
                    other.GetComponent<Rigidbody>().AddForce(Owner.transform.forward * (MeleeOBJ.RBKnockbackPower), ForceMode.Impulse);
                }
            }
        }
    }
}