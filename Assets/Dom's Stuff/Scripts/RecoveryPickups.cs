using UnityEngine;

public class RecoveryPickups : MonoBehaviour
{
    public PickupScriptableObject PickUpOBJ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PickUpOBJ.RecoverHealth)
            {
                other.GetComponent<Player>().HealPlayer(PickUpOBJ.RecoveryAmount);
            }
            if (PickUpOBJ.RecoverShield)
            {
                other.GetComponentInChildren<ParticleShield>().RecoverShield(PickUpOBJ.RecoveryAmount);
            }
            ObjectPooling.DeSpawn(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (PickUpOBJ.RecoverHealth)
            {
                collision.transform.GetComponent<Player>().HealPlayer(PickUpOBJ.RecoveryAmount);
            }
            if (PickUpOBJ.RecoverShield)
            {
                collision.transform.GetComponentInChildren<ParticleShield>().RecoverShield(PickUpOBJ.RecoveryAmount);
            }
            ObjectPooling.DeSpawn(gameObject);
        }
    }
}
