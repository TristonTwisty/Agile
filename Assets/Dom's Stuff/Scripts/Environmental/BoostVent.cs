using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostVent : MonoBehaviour
{
    [Tooltip("How much force is applied to the boost")][SerializeField] private float BoostForce = 20f;

    [Header("Player")]
    [SerializeField] private Transform Player;
    [SerializeField] private bool Testing;

    [Header("Which character controller?")]
    [SerializeField] private bool HasCharacterController;
    [SerializeField] private bool HasRigibody;

    private void Start()
    {
        if (!Testing)
        {
            Player = PlayerRefs.instance.Player;
        }

        if (Player.GetComponent<CharacterController>() != null)
        {
            HasCharacterController = true;
            HasRigibody = false;
        }
        else if (Player.GetComponent<Rigidbody>() != null)
        {
            HasRigibody = true;
            HasCharacterController = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (HasCharacterController)
            {
                other.GetComponent<CharacterController>().Move(transform.up * BoostForce);
            }
            else if (HasRigibody)
            {
                other.GetComponent<Rigidbody>().AddForce(transform.up * BoostForce, ForceMode.VelocityChange);
            }
        }
    }
}
