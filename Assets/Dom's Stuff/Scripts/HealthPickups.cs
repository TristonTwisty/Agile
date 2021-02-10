using UnityEngine;

public class HealthPickups : MonoBehaviour
{
    public PickupScriptableObject PickUpOBJ;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<PlayerHealth>().CurrentHealth += PickUpOBJ.HealthGain;
            Destroy(gameObject);
        }
    }
}
