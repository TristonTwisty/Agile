using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    public enum PowerUp { Disk, Shield };
    public PowerUp ActivePower = PowerUp.Disk;

    [SerializeField] private GameObject Bat;
    [SerializeField] private GameObject Shield;
    [SerializeField] private GameObject Disk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ActivePower == PowerUp.Disk)
            {
                other.GetComponent<PlayerMelee>().enabled = false;
                Disk.SetActive(true);
                Bat.SetActive(false);
            }
            if(ActivePower == PowerUp.Shield)
            {
                Shield.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
