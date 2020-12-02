using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    [HideInInspector] public float CurrentHealth = 0;

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
        if(CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void TakeDamage(float Damage)
    {
        CurrentHealth -= Damage;
    }
}
