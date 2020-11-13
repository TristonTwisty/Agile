using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    private float CurrentHealth = 0;

    void Start()
    {
        CurrentHealth = MaxHealth;
        gameObject.gameObject.tag = "Player";
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }
}
