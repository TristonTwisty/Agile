using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyScripableObject EnemyOBJ;
    public MeshRenderer MR;

    private float MaxHealth;
    [HideInInspector] public float CurrentHealth;

    private void Awake()
    {
        MaxHealth = EnemyOBJ.Health;
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }
}
