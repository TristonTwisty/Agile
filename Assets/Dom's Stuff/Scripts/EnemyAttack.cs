using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform Barrel;

    [HideInInspector] public ProjectileScriptableObjects ProjectileObj = null;
    [HideInInspector] public EnemyScripableObject EnemyOBJ = null;
    [HideInInspector] public Transform Target;
    private float TargetDistance = 0;
    private bool Attack = false;
    private bool CanAttack = false;
    private GameObject Projectile;

    private void Start()
    {
        Projectile = ProjectileObj.Projectile;
    }

    private void Update()
    {
        TargetDistance = Vector3.Distance(Target.position, transform.position);

        if(TargetDistance <= EnemyOBJ.AttackRange)
        {
            Attack = true;
        }
        else
        {
            Attack = false;
        }

        if(Attack && CanAttack)
        {
            StartCoroutine(DoAttack());
        }
        else if (Attack)
        {
            StartCoroutine(Focus());
        }
    }

    IEnumerator Focus()
    {
        yield return new WaitForSeconds(EnemyOBJ.FocusTime);
        CanAttack = true;
        print("die!");
    }
    IEnumerator DoAttack()
    {
        CanAttack = false;
        yield return new WaitForSeconds(EnemyOBJ.AttackRate);
        Instantiate(Projectile, Barrel.position, transform.rotation);
    }
}