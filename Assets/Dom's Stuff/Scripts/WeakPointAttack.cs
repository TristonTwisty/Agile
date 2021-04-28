using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPointAttack : MonoBehaviour
{
    [SerializeField] private FinalBoss Fb;
    public enum State { BatCollider, ThunderStrikeCollider, TargetCollider }
    public State ActiveState;

    private Collider collider;

    [Header("Bat Attack")]
    [SerializeField] private float PushForce = 10;

    [Header("Targeting Attack")]
    [SerializeField] private float ResetTimer = 20;
    [SerializeField] private Color ActivationColor;
    [HideInInspector] public bool Activated = false;
    private MeshRenderer MR;
    private Color OriginalColor;

    private void Start()
    {
        collider = GetComponent<Collider>();

        collider.isTrigger = true;
        collider.enabled = false;

        MR = GetComponent<MeshRenderer>();
        OriginalColor = MR.material.color;
    }

    private void Update()
    {
        if(Activated == true)
        {
            MR.material.color = ActivationColor;
        }
        else
        {
            MR.material.color = OriginalColor;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(ActiveState == State.ThunderStrikeCollider)
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponentInChildren<ParticleShield>().ShieldOn)
                {
                    Debug.Log("Player Protected");
                }
                else
                {
                    other.GetComponent<Player>().TakeDamage(Fb.ShieldAttackDamage);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(ActiveState == State.BatCollider)
        {
            if (other.CompareTag("Target"))
            {
                Debug.Log("block hit");
                Fb.EndAttack = true;
                Fb.EB.TakeDamage(1);
                Destroy(other);
            }
            else if (other.CompareTag("Player"))
            {
                Fb.EndAttack = true;
                //other.GetComponent<Player>().TakeDamage(Fb.BatAttackDamage);
                other.GetComponent<Rigidbody>().AddForce(Fb.transform.forward * PushForce, ForceMode.VelocityChange);
            }
        }

        if(ActiveState == State.TargetCollider)
        {
            if(other.CompareTag("Particle Disc"))
            {
                StartCoroutine(ActivationTimer());
            }
        }
    }

    private IEnumerator ActivationTimer()
    {
        Activated = true;

        yield return new WaitForSeconds(ResetTimer);

        Activated = false;
    }
}