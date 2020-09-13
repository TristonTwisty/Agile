using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RingToss : MonoBehaviour
{
    [Header("Behavior")]
    public float Damage;
    private GameObject Ring;
    [SerializeField] private Transform Player;
    [Tooltip("The particle system for when the ring is returning to the player")][SerializeField] private ParticleSystem RingDisperse;
    private bool CanThrow = true;
    private bool ReturnToPlayer = false;

    [Header("Arc Waypoints")]
    [SerializeField] private Transform RingWaypointOne;
    [SerializeField] private Transform RingWaypointTwo;
    [SerializeField] private Transform RingWaypointThree;

    [Header("Straight Shot Waypoint")]
    [SerializeField] private Transform StraightWaypoint;
    private Vector3 StraightAway;

    [Header("Tweener")]
    [Tooltip("The path type the disc will follow")][SerializeField] private PathType RingArc = PathType.CatmullRom;
    private Vector3[] Waypoints = new Vector3[3];
    [Tooltip("How fast the ring completes the arc")][SerializeField] private float ThrowSpeed = 5;
    [Tooltip("How fast the ring returns to player")] [SerializeField] private float ReturnSpeed = 1;

    private void Start()
    {
        Ring = gameObject;
    }

    private void Update()
    {
        SetWayPoints();

        //Right Click to toss the ring at an arc
        if (Input.GetMouseButtonDown(1) && CanThrow == true)
        {
            ArcShot();
        }

        // Left Click to toss ring forward
        if (Input.GetMouseButtonDown(0) && CanThrow == true)
        {
            StraightShot();
        }

        //Once the ring finishes it's arc the ring will return to player
        if (ReturnToPlayer)
        {
            ReturnRing();
        }
    }

    private void ArcShot()
    {
        StartCoroutine(ReturnTimer());
        CanThrow = false;
        Ring.transform.DOPath(Waypoints, ThrowSpeed, RingArc);
    }

    private void StraightShot()
    {
        StartCoroutine(ReturnTimer());
        CanThrow = false;
        Ring.transform.DOMove(StraightAway, ThrowSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If ring hits an Enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            DoDamage();
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Ring.GetComponent<MeshRenderer>().enabled = true;
            CanThrow = true;
        }
    }

    private void DoDamage()
    {

    }

    private void ReturnRing()
    {
        Ring.transform.position = Vector3.MoveTowards(transform.position, Player.position, ReturnSpeed);
    }

    private IEnumerator ReturnTimer()
    {
        yield return new WaitForSeconds(ThrowSpeed - ThrowSpeed);
        ReturnToPlayer = true;
    }

    private void SetWayPoints()
    {
        //Updates waypoints for arc shot
        Waypoints.SetValue(RingWaypointOne.position, 0);
        Waypoints.SetValue(RingWaypointTwo.position, 1);
        Waypoints.SetValue(RingWaypointThree.position, 2);

        //Set Waypoint for straight shot
        StraightAway = StraightWaypoint.position;
    }
}
