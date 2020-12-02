using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pickup SO", menuName = "Pickup Statistics")]
public class PickupScriptableObject : ScriptableObject
{
    public float HealthGain;
    public int DropChance;
    public GameObject PickupGameObject;
}
