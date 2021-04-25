using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup SO", menuName = "Pickup OBJ")]
public class PickupScriptableObject : ScriptableObject
{
    public bool RecoverShield;
    public bool RecoverHealth;
    public float RecoveryAmount;
    public int DropChance;
    public GameObject PickupGameObject;
}
