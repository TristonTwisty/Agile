using UnityEngine;

[CreateAssetMenu(fileName = "New Melee SO", menuName = "Melee Statistics")]

public class MeleeScriptableObject : ScriptableObject
{
    public GameObject MeleeWeapon;
    public float DamageDealth;
    public float HitBox;
}
