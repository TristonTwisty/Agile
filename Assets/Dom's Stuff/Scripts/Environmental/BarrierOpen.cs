using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierOpen : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private Transform MoveLocation;
    [SerializeField] private float MoveSpeed;

    private void Update()
    {
        if(Enemies == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveLocation.position, MoveSpeed * Time.deltaTime);
        }
    }
}