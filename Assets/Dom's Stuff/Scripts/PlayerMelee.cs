using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;

    [Header("Weapon")]
    [SerializeField] private Collider WeaponCollider = null;

    private bool CanAttack = true;

    private void Start()
    {
        WeaponCollider.enabled = false;
    }

    private void Update()
    {
        if (CanAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CanAttack = false;
                animator.SetTrigger("Attack");
            }
        }
    }

    private void StartAttack()
    {
        WeaponCollider.enabled = true;
    }

    private void StopAttack()
    {
        CanAttack = true;
        WeaponCollider.enabled = false;
    }
}
