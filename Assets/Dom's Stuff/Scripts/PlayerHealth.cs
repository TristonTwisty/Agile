using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    [HideInInspector] public float CurrentHealth = 0;

    private GameObject player;

    void Start()
    {
        CurrentHealth = MaxHealth;
        gameObject.gameObject.tag = "Player";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            player.GetComponent<Player>().LoadPlayer();
            CurrentHealth = MaxHealth;
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
