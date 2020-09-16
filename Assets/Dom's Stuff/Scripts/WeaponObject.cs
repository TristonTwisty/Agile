using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]

public class WeaponObject : ScriptableObject
{
    public string WeaponName;
    public string Description;
    public float AttackSpeed;
    public float Damage;
}
