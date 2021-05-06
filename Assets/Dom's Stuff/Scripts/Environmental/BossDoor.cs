using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [Header("The boss of this door")]
    [SerializeField] private GameObject Boss;
    [SerializeField] private bool NoExit;
    [SerializeField] private bool NoDoor;

    [Header("Entrance / Exit")]
    [SerializeField] private Transform Entrance;
    [SerializeField] private Transform Exit;
    [SerializeField] private float MovementSpeed = 2;

    [Header("Positions")]
    [SerializeField] private Transform EntranceOpenPosition;
    [SerializeField] private Transform EntranceClosePosition;
    [SerializeField] private Transform ExitOpenPosition;
    [SerializeField] private Transform ExitClosePosition;

    private void Start()
    {
        if (!NoDoor)
        {
            Entrance.parent = null;
            Entrance.position = EntranceOpenPosition.position;
        }

        if (!NoExit)
        {
            Exit.position = ExitOpenPosition.position;
            Exit.parent = null;
        }
        else
        {
            Exit = null;
        }
    }

    private IEnumerator Closing()
    {
        float t = 0;

        Vector3 EntranceStartPosition;
        Vector3 ExitStartPosition;

        if (!NoDoor)
        {
            EntranceStartPosition = Entrance.position;
        }
        else
        {
            EntranceStartPosition = Vector3.zero;
        }

        if (!NoExit)
        {
            ExitStartPosition = Exit.position;
        }
        else
        {
            ExitStartPosition = Vector3.zero;
        }

        while (t < MovementSpeed)
        {
            t += Time.deltaTime;

            if(!NoDoor)
            {
                Entrance.position = Vector3.Lerp(EntranceStartPosition, EntranceClosePosition.position, t / MovementSpeed);
            }

            if (!NoExit)
            {
                Exit.position = Vector3.Lerp(ExitStartPosition, ExitClosePosition.position, t / MovementSpeed);
            }
            yield return null;
        }
    }

    public void OpenDoor()
    {
        StartCoroutine(Opening());
    }

    private IEnumerator Opening()
    {
        float t = 0;

        Vector3 EntranceStartPosition = Entrance.position;
        Vector3 ExitStartPosition;

        if (!NoExit)
        {
            ExitStartPosition = Exit.position;
        }
        else
        {
            ExitStartPosition = Vector3.zero;
        }

        while (t < MovementSpeed)
        {
            t += Time.deltaTime;
            Entrance.position = Vector3.Lerp(EntranceStartPosition, EntranceOpenPosition.position, t / MovementSpeed);

            if (!NoExit)
            {
                Exit.position = Vector3.Lerp(ExitStartPosition, ExitOpenPosition.position, t / MovementSpeed);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;

            StartCoroutine(Closing());

            Boss.GetComponent<EnemyBehavior>().ActivateBoss = true;
        }
    }
}