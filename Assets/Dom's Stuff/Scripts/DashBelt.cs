using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBelt : MonoBehaviour
{
    private Transform Cam;

    [SerializeField] private float DashPower = 20;
    public int MaximumDashes = 3;
    [HideInInspector] public int CurrentDashes;
    [SerializeField] private float DashRecharge = 5;

    private Rigidbody RB;

    private void Start()
    {
        Cam = Camera.main.transform;
        RB = GetComponent<Rigidbody>();

        CurrentDashes = MaximumDashes;
    }

    private void Update()
    {
        if(CurrentDashes != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                RB.AddForce(transform.forward * DashPower, ForceMode.VelocityChange);
            }
        }

        if(CurrentDashes >= MaximumDashes)
        {
            CurrentDashes = MaximumDashes;
        }

        if (CurrentDashes <= MaximumDashes)
        {
            StartCoroutine(DashCharge());
        }
    }

    private IEnumerator DashCharge()
    {
        yield return new WaitForSeconds(DashRecharge);

        CurrentDashes += 1;
    }
}