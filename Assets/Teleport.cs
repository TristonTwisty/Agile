using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public GameObject Destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        PlayerRefs.instance.Player.position = Destination.transform.position;
        PlayerRefs.instance.Player.rotation = Destination.transform.rotation;

    }


}
