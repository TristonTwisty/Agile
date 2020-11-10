using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private EnemyScripableObject EnemyObject;
    [SerializeField] private ProjectileScriptableObjects ProjectileObject;
    [SerializeField] private Transform Player;

    private void Awake()
    {
        GetComponent<EnemyMovement>().EnemyOBJ = EnemyObject;
        GetComponent<EnemyMovement>().Target = Player;
        GetComponent<EnemyAttack>().Target = Player;
        GetComponent<EnemyAttack>().EnemyOBJ = EnemyObject;
        GetComponent<EnemyAttack>().ProjectileObj = ProjectileObject;
    }
}
