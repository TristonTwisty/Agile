using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [Header("Components")]
    private Animator animator;

    [Header("Weapon")]
    [SerializeField] private MeleeScriptableObject MeleeOBJ = null;
    [SerializeField] private GameObject Weapon = null;
    private Collider WeaponCollider = null;
    [Tooltip("How long is the weapon?")] [SerializeField] private float WeaponLength = 1;

    private bool CanAttack = true;

    private void Start()
    {
        WeaponCollider = Weapon.GetComponent<Collider>();
        WeaponCollider.enabled = false;
        WeaponCollider.isTrigger = true;

        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (CanAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
            }
        }

        if(Physics.Raycast(transform.position, transform.forward, WeaponLength))
        {
            Debug.Log("Bad guy!");
        }
    }

    private void StartAttack()
    {
        CanAttack = false;
        WeaponCollider.enabled = true;
    }

    private void StopAttack()
    {
        CanAttack = true;
        WeaponCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * MeleeOBJ.KnockbackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + new Vector3(0f, 1f, 0f), transform.forward * WeaponLength);
    }
}
