using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyObject : ScriptableObject
{
    public string EnemyName;
    public string Description;
    public float Speed;
    public float Health;
    public float Damage;
}
