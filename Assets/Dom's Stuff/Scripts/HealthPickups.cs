using UnityEngine;

public class HealthPickups : MonoBehaviour
{
    public PickupScriptableObject PickUpOBJ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().HealPlayer(PickUpOBJ.HealthGain);
            ObjectPooling.DeSpawn(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().HealPlayer(PickUpOBJ.HealthGain);
            ObjectPooling.DeSpawn(gameObject);
        }
    }
}
