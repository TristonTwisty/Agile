using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public PlayerRefs PlayerCheckPoint;
    private void OnTriggerEnter(Collider other)
    {
        PlayerCheckPoint.checkpoint = gameObject.transform;
    }
}
