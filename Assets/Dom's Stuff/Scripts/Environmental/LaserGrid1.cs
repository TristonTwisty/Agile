using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGrid1 : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private Transform StartPoint;
    [SerializeField] private LineRenderer LR;

    [Header("Interactable")]
    [SerializeField] private float KnockBackPower = 15;
    [SerializeField] private float LaserDamage = 10;

    private Collider collider;

    private void Start()
    {
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
                //Debug.Log("Sheild detected. disable collider");
                collider.enabled = false;
            }
            else
            {
                //Debug.Log("NO");
                collider.enabled = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collison detected");
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Enemy"))
        {
            //Debug.Log("Player or enemy");
            ContactPoint CP = collision.contacts[0];

            Vector3 Direction = (collision.transform.position - CP.point).normalized;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Direction * KnockBackPower, ForceMode.Impulse);
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            //Debug.Log("enemy");
            collision.transform.GetComponent<EnemyBehavior>().TakeDamage(LaserDamage);
        }
        else if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<Player>().TakeDamage(LaserDamage);
        }
    }
}
