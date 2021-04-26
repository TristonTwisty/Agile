using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSequence : MonoBehaviour
{
    public enum State { Boss, FirePlume, PitFall, Door, ShockTrap}
    public State ActiveState;

    [Header("Boss Movement")]
    [SerializeField] private Transform BossFleeTarget;
    private NavMeshAgent agent;

    [Header("Hazards")]
    [SerializeField] private float HazardDamage;

    private void Start()
    {
        switch (ActiveState)
        {
            case State.Boss:
                agent = GetComponent<NavMeshAgent>();
                agent.destination = BossFleeTarget.position;
                break;
            case State.FirePlume:
                break;
            case State.PitFall:
                OriginalPosition.parent = null;
                FallPosition.parent = null;
                transform.position = OriginalPosition.position;
                break;
            case State.Door:
                OpenPosition.parent = null;
                ClosePosition.parent = null;
                transform.position = OpenPosition.position;
                break;
            case State.ShockTrap:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            if (ActiveState == State.FirePlume)
            {
                firePlume();
            }
            else if (ActiveState == State.ShockTrap)
            {
                securityDoor();
            }
            else if(ActiveState == State.Door)
            {
                securityDoor();
            }
            else if (ActiveState == State.PitFall)
            {
                securityDoor();
            }
        }
    }

    #region Fire Plume Activation
    [Header("Fire Plume")]
    [SerializeField] private ParticleSystem Fire;
    private void firePlume()
    {
        Fire.Play();
    }
    #endregion

    #region ShockTrap
    [Header("ShockTrap")]
    [SerializeField] private float ShockTrapDamage = 10;
    [SerializeField] private ParticleSystem Electricity;
    private void shochTrap()
    {
        Electricity.Play();
    }
    #endregion

    #region Security Door
    [Header("Security Door")]
    [SerializeField] private Transform SecurityDoor;
    [SerializeField] private Transform ClosePosition;
    [SerializeField] private Transform OpenPosition;
    [SerializeField] private float DoorMovementSpeed = 10;

    private IEnumerator securityDoor()
    {
        float t = 0;

        Vector3 startPosition = transform.position;

        while (t < DoorMovementSpeed)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, ClosePosition.position, t / DoorMovementSpeed);
            yield return null;
        }
    }
    #endregion

    #region Pitfall
    [Header("Pit fall")]
    [SerializeField] private Transform Pit;
    [SerializeField] private Transform OriginalPosition;
    [SerializeField] private Transform FallPosition;
    [SerializeField] private float PitfallSpeed = 10;
    private IEnumerator pitFall()
    {
        float t = 0;

        Vector3 startPosition = transform.position;

        while (t < DoorMovementSpeed)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, FallPosition.position, t / PitfallSpeed);
            yield return null;
        }
    }
    #endregion
}
