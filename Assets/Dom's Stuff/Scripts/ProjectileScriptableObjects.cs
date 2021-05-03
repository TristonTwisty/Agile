using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile - ", menuName = "Projectile OBJ")]

public class ProjectileScriptableObjects : ScriptableObject
{
    public GameObject Projectile;
    public float DamageDealt = 10;
    public float ProjectileSpeed;
    public float ProjectileLifetime;
    public float MaxSpread;
    public ParticleSystem DestroyParticle;
}
