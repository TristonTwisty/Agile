using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostVent : MonoBehaviour
{
    [Tooltip("What direct the boost pushes that player")][SerializeField] private Vector3 Boost;
    [Tooltip("How much force is applied to the boost")][SerializeField] private float BoostForce;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody RB = other.gameObject.GetComponent<Rigidbody>();
        RB.AddForce(Boost * BoostForce, ForceMode.Impulse);
    }
}
