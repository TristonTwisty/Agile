using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystemDespawn : MonoBehaviour
{
    private ParticleSystem PS;

    private void OnEnable()
    {
        PS = GetComponent<ParticleSystem>();
        PS.Play();
        StartCoroutine(PSDespawn());
    }

    private IEnumerator PSDespawn()
    {
        yield return new WaitForSeconds(PS.main.duration);

        ObjectPooling.DeSpawn(gameObject);
    }
}
