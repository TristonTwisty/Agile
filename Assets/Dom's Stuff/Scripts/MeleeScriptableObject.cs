using UnityEngine;

[CreateAssetMenu(fileName = "New Melee SO", menuName = "Melee Statistics")]

public class MeleeScriptableObject : ScriptableObject
{
    public float DamageDealt;
    public float KnockbackPower;
}
