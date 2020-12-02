using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierOpen : MonoBehaviour
{
    private GameObject[] Enemies;

    private void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        if(Enemies.Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
