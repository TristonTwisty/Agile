using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RingToss : MonoBehaviour
{
    [Header("Behavior")]
    [SerializeField] private GameObject Ring;
    [SerializeField] private Transform Player;
    [Tooltip("The particle system for when the ring is returning to the player")][SerializeField] private ParticleSystem RingDisperse;
    private bool CanThrow = true;

    [Header("Waypoints")]
    [SerializeField] private GameObject RingWaypointOne;
    [SerializeField] private GameObject RingWaypointTwo;
    [SerializeField] private GameObject RingWaypointThree; //This waypoint hould be tagged as "ArcEnd"
    [SerializeField] private GameObject WaypointBaseOne;
    [SerializeField] private GameObject WaypointBaseTwo;
    [SerializeField] private GameObject WaypointBaseThree;
    private Vector3 WaypointOne;
    private Vector3 WaypointTwo;
    private Vector3 WaypointThree;

    [Header("Tweener")]
    [Tooltip("The path type the disc will follow")][SerializeField] private PathType Arc = PathType.CatmullRom;
    private Vector3[] Waypoints = new Vector3[3];
    [Tooltip("How fast the ring completes the arc")][SerializeField] private float ThrowSpeed = 5;
    [Tooltip("How fast the ring returns to player")] [SerializeField] private float ReturnSpeed = 1;

    private void Update()
    {

        SetWaypoints();

        //Right Click to toss the ring at an arc
        if (Input.GetMouseButtonDown(1) && CanThrow == true)
        {
            CanThrow = false;
            // Waypoints are unparented from player so ring completes it's arc
            RingWaypointOne.transform.parent = null;
            RingWaypointTwo.transform.parent = null;
            RingWaypointThree.transform.parent = null;
            Ring.transform.DOPath(Waypoints, ThrowSpeed, Arc);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ArcEnd"))
        {
            // Ring completes it's tween
            Ring.transform.DOComplete();
            // Ring's particle system is unparented and plays at ArcEnd
            RingDisperse.transform.parent = null;
            RingDisperse.Play();
            // Ring disapears
            Ring.GetComponent<MeshRenderer>().enabled = false;

            // Waypoints are parented and moved to WaypointBases so they follow the player again
            RingWaypointOne.transform.parent = WaypointBaseOne.transform;
            RingWaypointOne.transform.localPosition = Vector3.zero;

            RingWaypointTwo.transform.parent = WaypointBaseTwo.transform;
            RingWaypointTwo.transform.localPosition = Vector3.zero;

            RingWaypointThree.transform.parent = WaypointBaseThree.transform;
            RingWaypointThree.transform.localPosition = Vector3.zero;

            // Ring waits a couple seconds before returning to player
            StartCoroutine(RingRespawn());
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Ring.GetComponent<MeshRenderer>().enabled = true;
            CanThrow = true;
        }
    }

    private void SetWaypoints()
    {
        WaypointOne.Set(RingWaypointOne.transform.position.x, RingWaypointOne.transform.position.y, RingWaypointOne.transform.position.z);
        WaypointTwo.Set(RingWaypointTwo.transform.position.x, RingWaypointTwo.transform.position.y, RingWaypointTwo.transform.position.z);
        WaypointThree.Set(RingWaypointThree.transform.position.x, RingWaypointThree.transform.position.y, RingWaypointThree.transform.position.z);

        Waypoints.SetValue(WaypointOne, 0);
        Waypoints.SetValue(WaypointTwo, 1);
        Waypoints.SetValue(WaypointThree, 2);
    }

    private IEnumerator RingRespawn()
    {
        yield return new WaitForSeconds(ReturnSpeed);
        // Ring returns to player
        Ring.transform.localPosition = Vector3.zero;
        // Ring's Particle system returned to ring
        RingDisperse.transform.parent = Ring.transform;
        RingDisperse.transform.localPosition = Vector3.zero;
    }
}
