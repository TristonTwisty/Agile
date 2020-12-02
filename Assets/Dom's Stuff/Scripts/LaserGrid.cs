using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGrid : MonoBehaviour
{
    private LineRenderer LaserRenderer;
    [SerializeField] private float LaserWidth;
    [SerializeField] private float LaserLength;

    private void Start()
    {
        LaserRenderer = GetComponent<LineRenderer>();

        Vector3[] InitLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        LaserRenderer.SetPositions(InitLaserPositions);
        LaserRenderer.startWidth = LaserWidth;
        LaserRenderer.endWidth = LaserWidth;
    }

    private void Update()
    {
        ShootLaserFromPosition(transform.position, Vector3.forward, LaserLength);
    }

    private void ShootLaserFromPosition(Vector3 TargetPosition, Vector3 Direction, float Length)
    {
        Ray ray = new Ray(TargetPosition, Direction);
        RaycastHit hit;
        Vector3 EndPosition = TargetPosition + (Length * Direction);

        if(Physics.Raycast(ray , out hit , Length))
        {
            EndPosition = hit.point;
        }

        LaserRenderer.SetPosition(0, TargetPosition);
        LaserRenderer.SetPosition(1, EndPosition);

        //hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(MoveDirection.normalized * -500);
    }
}
