using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public PlayerRefs instance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            other.GetComponent<Player>().SavePlayer();
            gameObject.SetActive(false);
        }
    }
}
