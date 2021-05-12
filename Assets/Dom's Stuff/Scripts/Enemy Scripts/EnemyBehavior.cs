using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Component")]
    public EnemyScripableObject EnemyOBJ;

    private float MaxHealth;
    public float CurrentHealth;

    [HideInInspector] public bool ActivateBoss;

    [SerializeField] private ParticleSystem DamagedPS;
    [Header("Testing")]
    [SerializeField] private bool Testing;
    [SerializeField] private bool Invincible;

    private void Awake()
    {
        MaxHealth = EnemyOBJ.Health;
        CurrentHealth = MaxHealth;

        if (gameObject.CompareTag("Boss"))
        {
            Debug.Log("Manually set particle system");
        }
        else
        {
            DamagedPS = GetComponentInChildren<ParticleSystem>();
        }
    }

    public void TakeDamage(float Damage)
    {
        if (!Invincible)
        {
            if (!DamagedPS.isPlaying)
            {
                DamagedPS.Play();
            }
            CurrentHealth -= Damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.AttackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyOBJ.ChaseRange);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Testing)
        {
            TakeDamage(100);
        }
    }
}
