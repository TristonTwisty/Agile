using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyScripableObject EnemyOBJ;

    private float MaxHealth;
    [HideInInspector] public float CurrentHealth;

    [HideInInspector] public bool ActivateBoss;

    private void Awake()
    {
        MaxHealth = EnemyOBJ.Health;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.ChaseRange);
    }
}
