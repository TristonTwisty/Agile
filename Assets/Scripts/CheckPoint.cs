using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public PlayerRefs refs;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            refs = other.GetComponent<PlayerRefs>();
            refs.checkpoint = gameObject.transform;
            other.GetComponent<Player>().SavePlayer();
        }
        gameObject.SetActive(false);
    }
}
