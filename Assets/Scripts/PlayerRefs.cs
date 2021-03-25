using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRefs : MonoBehaviour
{

    public static PlayerRefs instance;
    public Transform PlayerBatt;
    public Transform PlayerWhip;
    public Transform Disk;
    public Transform Player;
    public Transform Sheild;
    public Transform Batt;
    public RigidbodyFPS movementS;
    public Transform PlayerCamera;
    public Transform RingHolster;
    public Transform checkpoint;
    public float PlayerHealth;

    private void Awake()
    {
        if (PlayerRefs.instance == null)
        {
            PlayerRefs.instance = this;
        }
        else if (PlayerRefs.instance != this)
        {
            Destroy(this);
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
