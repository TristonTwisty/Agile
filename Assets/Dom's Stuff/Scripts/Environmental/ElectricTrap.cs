using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : MonoBehaviour
{
    [Header("Trap Properties")]
    [SerializeField] private float DamageDealt = 5;
    [SerializeField] private ParticleSystem Electrcity;
    [SerializeField] private float ActivationTime = 5;
    private new Collider collider;
    /* [HideInInspector] */ public bool DestroyTrap = false;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        collider.enabled = false;
    }
    public void ActivateTrap()
    {
        if (!DestroyTrap)
        {
            StartCoroutine(Trap());
        }
    }

    private IEnumerator Trap()
    {
        if (!DestroyTrap)
        {
            int ActivationChance = Random.Range(0, 2);

            Debug.Log(ActivationChance);

            if (ActivationChance == 0)
            {
                collider.enabled = true;
                Electrcity.Play();
            }
            else if (ActivationChance == 1)
            {
                collider.enabled = false;
                Electrcity.Stop();
            }
        }

        else if (DestroyTrap)
        {
            Electrcity.Stop();
            collider.enabled = false;
        }

        yield return new WaitForSeconds(ActivationTime);

        if (!DestroyTrap)
        {
            StartCoroutine(Trap());
        }

        else if (DestroyTrap)
        {
            Electrcity.Stop();
            collider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(DamageDealt);
        }

        if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBehavior>().TakeDamage(DamageDealt);
        }
    }
}