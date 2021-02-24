using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofBounds : MonoBehaviour
{
    [SerializeField] private Transform SpawmPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = SpawmPoint.position;
            other.transform.rotation = SpawmPoint.rotation;
        }
    }
}
