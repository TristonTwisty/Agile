using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBelt : MonoBehaviour
{
    [SerializeField] private float DashPower = 20;
    public int MaximumDashes = 3;
    //[HideInInspector] 
    public int CurrentDashes;
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
        if (HasDashBelt)
        {
            if (CurrentDashes > 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    StartCoroutine(Dash());
                }
            }
        }

        if(CurrentDashes < 0)
        {
            CurrentDashes = 0;
        }

        if(CurrentDashes > MaximumDashes)
        {
            CurrentDashes = MaximumDashes;
        }
    }

    private IEnumerator Dash()
    {
        RB.AddForce(transform.forward * DashPower, ForceMode.Impulse);
        CurrentDashes -= 1;

        yield return new WaitForSeconds(DashRecharge);

        CurrentDashes += 1;
    }
}