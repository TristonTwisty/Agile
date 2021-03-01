using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGrid1 : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private Transform StartPoint;
    private LineRenderer LR;
    [SerializeField] private LayerMask IgnoreMask;

    [Header("Mesh")]
    private MeshCollider MC;

    private void Start()
    {
        LR = GetComponent<LineRenderer>();
        LR.startWidth = .5f;

        MC = GetComponent<MeshCollider>();

        LR.SetPosition(0, StartPoint.position);
    }

    private void Update()
    {
        Mesh mesh = new Mesh();

        if (Physics.Raycast(StartPoint.position, StartPoint.right, out RaycastHit hit, Mathf.Infinity, ~IgnoreMask))
        {
            LR.SetPosition(1, hit.point);

            LR.BakeMesh(mesh, true);
            MC.sharedMesh = mesh;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
        {
            ContactPoint CP = collision.contacts[0];

            Vector3 Direction = (collision.transform.position - CP.point).normalized;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Direction * 20, ForceMode.Impulse);
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyBehavior>().TakeDamage(50);
        }
    }
}
