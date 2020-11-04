using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public EnemyObject enemy;
    private EnemyMovement movement;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Agent.speed = enemy.Speed;
    }
}
