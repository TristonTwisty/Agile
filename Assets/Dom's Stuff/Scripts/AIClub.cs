using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIClub : MonoBehaviour
{
    public MeleeScriptableObject MeleeOBJ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(MeleeOBJ.DamageDealth);
        }
    }
}
