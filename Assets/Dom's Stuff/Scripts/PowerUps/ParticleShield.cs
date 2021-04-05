﻿using System.Collections;
using UnityEngine;

public class ParticleShield : MonoBehaviour
{
    public float MaxCapacity = 100;
    [SerializeField] private float DrainSpeed = 10;
    public float RechargeSpeed = 2.5f;
    [Tooltip("The PS when the shield is activated")] public ParticleSystem ShieldPS;
    [Tooltip("The PS activated when something hits the shield")] public GameObject DeflectPS;
    public float CurrentCapacity;
    private Rigidbody ShieldBody;
    private Collider Collider;
    private MeshRenderer MeshRend;

    //Added By Ricardo For U.I.
    //public Scriptforui scriptForUI;
    //public GameSounds gameSounds;

    private void OnEnable()
    {
        gameObject.layer = 13;
        gameObject.tag = "Sheild";

        //Debug.Log("Enable called");
        CurrentCapacity = MaxCapacity;

        ShieldBody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        MeshRend = GetComponent<MeshRenderer>();

        // Disable meshrenderer and collider at start
        Collider.enabled = false;
        MeshRend.enabled = false;


        //Added By Ricardo For U.I.
        //shieldOn = false;
        //scriptForUI = GameObject.FindObjectOfType<Scriptforui>();
        //gameSounds = GameObject.FindObjectOfType<GameSounds>();
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (CurrentCapacity > 0)
            {
                CurrentCapacity -= DrainSpeed * Time.deltaTime;

                MeshRend.enabled = true;
                Collider.enabled = true;
                ShieldPS.Play();
            }
            else
            {
                MeshRend.enabled = false;
                Collider.enabled = false;
                ShieldPS.Play();
            }
        }
        else
        {
            if (CurrentCapacity < MaxCapacity)
            {
                CurrentCapacity += RechargeSpeed * Time.deltaTime;
            }

            MeshRend.enabled = false;
            Collider.enabled = false;
            ShieldPS.Stop();
        }

        if(CurrentCapacity > MaxCapacity)
        {
            CurrentCapacity = MaxCapacity;
        }
        if(CurrentCapacity < 0)
        {
            CurrentCapacity = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //Find where the point of contact
            ContactPoint Contact = collision.contacts[0];

            //Play PS at point of contact
            ObjectPooling.Spawn(DeflectPS, new Vector3(Contact.point.x, Contact.point.y, Contact.point.z), Quaternion.identity);

            //Destroy object hit
            Destroy(collision.gameObject);
        }
    }
}
