using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public Transform Player;
    [Tooltip("All enemies triggered when stepping into this zone")] public GameObject[] Enemies;

    private void Awake()
    {
        foreach (GameObject enemy in Enemies)
        {
            // Gives all enemies the player transform 
            enemy.GetComponent<EnemyMovement>().Target = Player;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(GameObject enemy in Enemies)
            {
                enemy.GetComponent<EnemyMovement>().AttackPlayer = true;
            }
        }
    }
}
