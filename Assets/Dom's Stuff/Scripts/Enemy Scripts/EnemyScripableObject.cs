using UnityEngine;

[CreateAssetMenu(fileName = "Enemy - ", menuName = "Enemy Type")]
public class EnemyScripableObject : ScriptableObject
{
    public string Description;
    public float Health;
    public float MovementSpeed;
    public float AttackRate;
    public float AttackRange;
    public float ChaseRange;

    [Header("Shooter")]
    public float BulletsPerShot;

    [Header("Scriptable Objects")]
    public ProjectileScriptableObjects ProjectileOBJ;
    public MeleeScriptableObject MeleeObj;
    public PickupScriptableObject PickupOBJ;
}