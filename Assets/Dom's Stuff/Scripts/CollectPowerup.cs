using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerup : MonoBehaviour
{
    [SerializeField] private GameObject ParticleDisc = null;
    [SerializeField] private bool CollectDisc = false;

    [SerializeField] private GameObject ParticleShield = null;
    [SerializeField] private bool CollectShield = false;

    [SerializeField] private GameObject DashBelt = null;
    [SerializeField] private bool CollectBelt = false;

    //[SerializeField] private GameObject Hoverboard = null;
    //[SerializeField] private bool CollectBoard;

    [SerializeField] private GameObject MagneticBoots = null;
    [SerializeField] private bool CollectBoots = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CollectShield)
            {
                ParticleShield.SetActive(true);
                Destroy(gameObject);
            }
            if (CollectBelt)
            {
                DashBelt.SetActive(true);
                Destroy(gameObject);
            }
            if (CollectDisc)
            {
                ParticleDisc.SetActive(true);
                Destroy(gameObject);
            }
            if (CollectBoots)
            {
                MagneticBoots.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
