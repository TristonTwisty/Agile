﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShield : MonoBehaviour
{
    public float MaxCapacity;
    public float DrainSpeed;
    public float RechargeSpeed;
    [Tooltip("The PS when the shield is activated")] public ParticleSystem ShieldPS;
    [Tooltip("The PS activated when something hits the shield")] public ParticleSystem DeflectPS;
    private float CurrentCapacity;
    private Rigidbody ShieldBody;
    private BoxCollider Collider;
    private MeshRenderer MeshRend;


    //Added By Ricardo for U.I.
    public bool shieldOn;


    //Added By Ricardo For U.I.
    public Scriptforui scriptForUI;
    public GameSounds gameSounds;


    private void Start()
    {
        CurrentCapacity = MaxCapacity;

        ShieldBody = GetComponent<Rigidbody>();
        Collider = GetComponent<BoxCollider>();
        MeshRend = GetComponent<MeshRenderer>();

        // Disable meshrenderer and collider at start
        Collider.enabled = false;
        MeshRend.enabled = false;


        //Added By Ricardo For U.I.
        shieldOn = false;
        scriptForUI = GameObject.FindObjectOfType<Scriptforui>();
        gameSounds = GameObject.FindObjectOfType<GameSounds>();
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // Enable PS, meshrenderer and collider when in use
            ShieldPS.Play();
            MeshRend.enabled = true;
            Collider.enabled = true;
            CurrentCapacity -= DrainSpeed;

            //Added By Ricardo For U.I.
            scriptForUI.shieldRecharge = scriptForUI.shieldRecharge - .3f;
            shieldOn = true;
            gameSounds.audioSource.PlayOneShot(gameSounds.shieldActivated);
        }

        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            ShieldPS.Stop();
            MeshRend.enabled = false;
            Collider.enabled = false;
            ShieldRecharge();
            shieldOn = false;

            //Added By Ricardo For U.I.
            gameSounds.audioSource.Stop();
 
        }

        if(CurrentCapacity <= 0)
        {
            //Disable everything when shield reaches 0
            CurrentCapacity = 0;
            ShieldPS.Stop();
            MeshRend.enabled = false;
            Collider.enabled = false;
            ShieldRecharge();
        }

        if(CurrentCapacity >= MaxCapacity)
        {
            CurrentCapacity = MaxCapacity;
        }

        //Added By Ricardo For U.I.
        if(shieldOn == false & scriptForUI.shieldRecharge < 100)
        {
            scriptForUI.shieldRecharge = scriptForUI.shieldRecharge + 0.3f;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //Find where the point of contact
            var Contact = collision.contacts[0];

            //Play PS at point of contact
            Instantiate(DeflectPS, new Vector3(Contact.point.x, Contact.point.y, Contact.point.z), Quaternion.identity);

            //Destroy object hit
            Destroy(collision.gameObject);
        }
    }

    private void ShieldRecharge()
    {
        CurrentCapacity += RechargeSpeed;
    }
}
