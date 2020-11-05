using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy SO", menuName = "Enemy Statistics")]
public class EnemyScripableObject : ScriptableObject
{
    public string EnemyName;
    public string Description;
    public float Health;
    public float MovementSpeed;
    public float AttackRate;
    public float DamageDealt;
    public float DamageResistance;
}
