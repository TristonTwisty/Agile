using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            other.GetComponent<Player>().HealPlayer(100);
            //other.GetComponent<Player>().SavePlayer();
            //gameObject.SetActive(false);
        }
    }
}
