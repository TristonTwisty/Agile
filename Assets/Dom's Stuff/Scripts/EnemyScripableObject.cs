using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy SO", menuName = "Enemy Statistics")]
public class EnemyScripableObject : ScriptableObject
{
    public string EnemyName;
    public string Description;
    public float Health;
    public float MovementSpeed;
    public float AttackRate;
    public float AttackRange;
    public float DamageDealt;
    public ProjectileScriptableObjects ProjectileOBJ;
}