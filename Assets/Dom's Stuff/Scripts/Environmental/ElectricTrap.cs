using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : MonoBehaviour
{
    [SerializeField] private ParticleSystem Electrcity;
    [SerializeField] private float ActivationTime = 15;
    [SerializeField] private float RaiseSpeed = 1;
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        collider.enabled = false;
    }
    public void ActivateTrap()
    {
        StartCoroutine(Trap());
    }

    private IEnumerator Trap()
    {
        Electrcity.Play();
        collider.enabled = true;

        yield return new WaitForSeconds(ActivationTime);

        Electrcity.Stop();
        collider.enabled = false;
    }
}