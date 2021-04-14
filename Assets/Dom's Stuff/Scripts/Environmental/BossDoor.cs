using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [Header("The boss of this door")]
    [SerializeField] private GameObject Boss;

    [Header("Entrance / Exit")]
    [SerializeField] private Transform Entrance;
    [SerializeField] private Transform Exit;

    [Header("Positions")]
    [SerializeField] private Transform EntranceOpenPosition;
    [SerializeField] private Transform EntranceClosePosition;
    [SerializeField] private Transform ExitOpenPosition;
    [SerializeField] private Transform ExitClosePosition;

    private void Start()
    {
        Entrance.parent = null;
        Exit.parent = null;

        Entrance.position = EntranceOpenPosition.position;
        Exit.position = ExitOpenPosition.position;
    }

    private void Awake()
    {
        Entrance.parent = null;
        Exit.parent = null;

        Entrance.position = EntranceOpenPosition.position;
        Exit.position = ExitOpenPosition.position;
    }

    private IEnumerator Closing()
    {
        float t = 0;

        Vector3 EntranceStartPosition = Entrance.position;
        Vector3 ExitStartPosition = Exit.position;

        while (t < 2)
        {
            t += Time.deltaTime;
            Entrance.position = Vector3.Lerp(EntranceStartPosition, EntranceClosePosition.position, t / 2);
            Exit.position = Vector3.Lerp(ExitStartPosition, ExitClosePosition.position, t / 2);
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
        Vector3 ExitStartPosition = Exit.position;

        while (t < 2)
        {
            t += Time.deltaTime;
            Entrance.position = Vector3.Lerp(EntranceStartPosition, EntranceOpenPosition.position, t / 2);
            Exit.position = Vector3.Lerp(ExitStartPosition, ExitOpenPosition.position, t / 2);
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