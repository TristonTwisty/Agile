using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    // Start is called before the first frame update

    public Grappling grapplinggun;

    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (!grapplinggun.IsGrappling()) {
            desiredRotation = transform.parent.rotation;
        }

        else {
            desiredRotation = Quaternion.LookRotation(grapplinggun.GetGrapplePoint() - transform.position);
        }


        transform.rotation = Quaternion.Lerp(a: transform.rotation, b: desiredRotation, t: Time.deltaTime * rotationSpeed);

        //transform.LookAt(worldPosition: grapplinggun.GetGrapplePoint());
    }
}
