using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBelt : MonoBehaviour
{
    [SerializeField] private float DashPower = 20;
    public int MaximumDashes = 3;
    [HideInInspector] public int CurrentDashes;
    [SerializeField] private float DashRecharge = 5;
    public bool HasDashBelt;

    private Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();

        CurrentDashes = MaximumDashes;
    }

    private void Update()
    {
        float DashCooling = DashRecharge;

        if (HasDashBelt)
        {
            if (CurrentDashes != 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    CurrentDashes -= 1;
                    DashCooling = DashRecharge;
                    RB.AddForce(transform.forward * DashPower, ForceMode.Impulse);
                }
            }

            if (CurrentDashes >= MaximumDashes)
            {
                CurrentDashes = MaximumDashes;
            }

            if (CurrentDashes < MaximumDashes)
            {
                DashCooling -= Time.deltaTime;
                if (DashCooling <= 0)
                {
                    CurrentDashes += 1;
                    DashCooling = 0;
                }
            }
        }
    }
}