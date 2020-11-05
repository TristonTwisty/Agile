using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public EnemyScripableObject EnemyOBJ;
    private EnemyMovement movement;

    private void Start()
    {
        movement = GetComponent<EnemyMovement>();
        movement.Agent.speed = EnemyOBJ.MovementSpeed;
    }
}
