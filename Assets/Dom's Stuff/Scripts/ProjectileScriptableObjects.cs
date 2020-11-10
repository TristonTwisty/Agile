using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile SO", menuName = "Projectile Statistics")]

public class ProjectileScriptableObjects : ScriptableObject
{
    public GameObject Projectile;
    public float DamageDealt;
    public float ProjectileSpeed;
}
