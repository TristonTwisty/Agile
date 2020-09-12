using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeathBalls : MonoBehaviour
{
    public Transform Player;
    public float Speed;
    private GameObject Self;

    private void Start()
    {
        Vector3 PlayerPOS = Player.position;
        gameObject.transform.DOMove(PlayerPOS, Speed);
    }


    private void Update()
    {

    }
}
