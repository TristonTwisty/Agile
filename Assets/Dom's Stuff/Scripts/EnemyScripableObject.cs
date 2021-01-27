using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy SO", menuName = "Enemy Statistics")]
public class EnemyScripableObject : ScriptableObject
{
    public string Description;
    public float Health;
    public float MovementSpeed;
    public float AttackRate;
    public float AttackRange;
    public float ChaseRange;
    public ProjectileScriptableObjects ProjectileOBJ;
    public MeleeScriptableObject MeleeObj;
    public PickupScriptableObject PickupOBJ;
}