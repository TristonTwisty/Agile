using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRingToss : MonoBehaviour
{
    [SerializeField] private float ThrowSpeed;
    [SerializeField] private float ReturnSpeed;
    //[SerializeField] private int ReflectionCount;
    [Tooltip("How far the disc can go before automatically returning to player")][SerializeField] private float ReturnDistance;
    private bool IsBoucning = false; //Marked true after first collision
    private bool CanThrow = true; //Can player throw disc?
    private bool Thrown = false; //Was the disc thrown?
    private Vector3 NewVelocity; //The vector of the reflected object

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanThrow)
        {
            CanThrow = false;
            Thrown = true;
        }
        if (Thrown)
        {
            DiscThrow();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        NewVelocity = Vector3.Reflect(transform.position, collision.contacts[0].normal);
        Bounce();
    }
    private void DiscThrow()
    {
        transform.position = transform.position + Camera.main.transform.forward * ThrowSpeed * Time.deltaTime;
    }
    private void Bounce()
    {
        //transform.Translate(NewVelocity * Time.deltaTime, Space.World);
        transform.position = Vector3.MoveTowards(transform.position, NewVelocity, ThrowSpeed);
    }
}
