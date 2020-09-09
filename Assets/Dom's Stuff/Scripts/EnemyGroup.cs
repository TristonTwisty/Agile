using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public Transform Player;
    public GameObject[] Enemies;

    private void Awake()
    {
        foreach (GameObject enemy in Enemies)
        {
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
