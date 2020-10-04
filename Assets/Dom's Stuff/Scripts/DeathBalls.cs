using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBalls : MonoBehaviour
{
    public Transform Player;
    public float Speed;
    private GameObject Self;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, Speed);
    }
}
