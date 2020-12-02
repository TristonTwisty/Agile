using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100;
    [HideInInspector] public float CurrentHealth = 0;

    private GameObject Inventory;

    void Start()
    {
        CurrentHealth = MaxHealth;
        gameObject.gameObject.tag = "Player";
        Inventory = GameObject.FindGameObjectWithTag("Inventory");
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            Inventory.GetComponent<Inventory>().LoadPlayer();
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
