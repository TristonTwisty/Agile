using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostVent : MonoBehaviour
{
    [Tooltip("What direct the boost pushes that player")] [SerializeField] private Vector3 Boost = new Vector3(0f,0f,0f);
    [Tooltip("How much force is applied to the boost")][SerializeField] private float BoostForce = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody RB = other.gameObject.GetComponent<Rigidbody>();
            RB.AddForce(Boost * BoostForce, ForceMode.Impulse);
        }
    }
}
