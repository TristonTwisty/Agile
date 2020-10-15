using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerup : MonoBehaviour
{
    [SerializeField] private GameObject Shield;
    [SerializeField] private bool CollectShield = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CollectShield)
            {
                Shield.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
