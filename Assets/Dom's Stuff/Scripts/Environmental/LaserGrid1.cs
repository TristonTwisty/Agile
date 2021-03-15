using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGrid1 : MonoBehaviour
{
    [SerializeField][Tooltip("How much force will the grid push the player back?")] private float KnockBackPower = 50;
    [Header("Line")]
    [SerializeField] private Transform StartPoint;
    [SerializeField] private LineRenderer LR;

    private Collider collider;

    private void Start()
    {
        //LR = GetComponent<LineRenderer>();
        LR.startWidth = .5f;

        collider = GetComponent<Collider>();

        LR.SetPosition(0, StartPoint.position);

        LR.useWorldSpace = true;
    }

    private void Update()
    {

        if (Physics.Raycast(StartPoint.position, StartPoint.right, out RaycastHit hit, Mathf.Infinity))
        {
            LR.SetPosition(1, hit.point);

            if (hit.transform.CompareTag("Sheild"))
            {
                collider.enabled = false;
            }
            else
            {
                collider.enabled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
        {
            ContactPoint CP = collision.contacts[0];

            Vector3 Direction = (collision.transform.position - CP.point).normalized;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Direction * KnockBackPower, ForceMode.Impulse);
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyBehavior>().TakeDamage(50);
        }
    }
}
