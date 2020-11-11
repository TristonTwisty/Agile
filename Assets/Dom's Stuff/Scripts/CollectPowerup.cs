using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPowerup : MonoBehaviour
{
    public enum Powerup { DashBelt, Shield, ParticleDisc, MagneticBoots };
    public Powerup GetPowerup;

    private GameObject Player = null;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player = other.gameObject;
            switch (GetPowerup)
            {
                case Powerup.Shield:
                    GetShield();
                    break;
                case Powerup.ParticleDisc:
                    GetDisc();
                    break;
                case Powerup.MagneticBoots:
                    GetBoots();
                    break;
                case Powerup.DashBelt:
                    GetBelt();
                    break;
            }
        }
        yield return null;
    }

    private void GetShield()
    {
        Player.GetComponent<ParticleShield>().enabled = true;
    }

    private void GetDisc()
    {
        Player.GetComponent<NewRingToss>().enabled = true;
    }

    private void GetBoots()
    {

    }

    private void GetBelt()
    {

    }
}

