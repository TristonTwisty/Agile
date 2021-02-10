using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnable : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<DashBelt>().enabled = true;
        Destroy(gameObject);
    }
}
