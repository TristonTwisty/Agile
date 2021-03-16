using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostVent : MonoBehaviour
{
    [Tooltip("How much force is applied to the boost")][SerializeField] private float BoostForce = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * BoostForce, ForceMode.Impulse);
        }
    }
}
