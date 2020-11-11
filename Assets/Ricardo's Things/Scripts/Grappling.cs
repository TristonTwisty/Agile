using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour

{
    public LineRenderer grapplingLine;
    public Vector3 grapplingPoint;
    public LayerMask grapplingMask;
    public Transform gunTip, camera, player;
    public float maxDistance = 100f;
    public SpringJoint joint;
    public Inventory script;








    // Start is called before the first frame update
    void Start()
    {
        grapplingLine = GetComponent<LineRenderer>();
        script = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        camera = script.PlayerCamera.transform;
        player = script.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }


    void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(origin: camera.position, direction:camera.forward, out hit, maxDistance, grapplingMask))
        {
            grapplingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplingPoint;


            float distanceFromPoint = Vector3.Distance(a: player.transform.position, b: grapplingPoint);

            //The distance grapple will try to keep from grapple point
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //spring values
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            grapplingLine.positionCount = 2;
            
        }
        else
        {
            Debug.Log("not on grapplingMask layer");
        }
        
    }


    void DrawRope()
    {
        if (!joint) return;
        grapplingLine.SetPosition(index: 0, gunTip.position);
        grapplingLine.SetPosition(index: 1, grapplingPoint);
    }

    void StopGrapple()
    {
        grapplingLine.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;

    }

    public Vector3 GetGrapplePoint()
    {
        return grapplingPoint;
    }


}
