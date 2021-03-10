using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBoss : MonoBehaviour
{
    [SerializeField] private Transform Player;

    [Header("Components")]
    private EnemyScripableObject EnemyOBJ;
    [SerializeField] private ParticleSystem AttackPS;
    [SerializeField] private Transform Head;

    [Header("Statistics")]
    private float AttackTimer;
    public float DamageDealt = 10;
    private float TurnSpeed;
    private Quaternion OriginalRot;

    // States
    [HideInInspector] public enum State {Initial, ChooseAttack, DirectAttack, SpinAttack, Dead, ResetRotation }
    [HideInInspector] public State ActiveState = State.ChooseAttack;

    [Header("Bools")]
    private bool IsAlive = true;
    private bool IsAttacking = false;

    private IEnumerator Start()
    {
        while (IsAlive)
        {
            switch (ActiveState)
            {
                case State.Initial:
                    DoInitial();
                    break;
                case State.ChooseAttack:
                    DoChooseAttack();
                    break;
                case State.DirectAttack:
                    DoDirectAttack();
                    break;
                case State.SpinAttack:
                    DoSpinAttack();
                    break;
                case State.Dead:
                    DoDead();
                    break;
                case State.ResetRotation:
                    StartCoroutine(ResetRotation());
                    break;
            }
            yield return null;
        }
    }

    private void DoInitial()
    {
        //Player = PlayerRefs.instance.Player;
        EnemyOBJ = GetComponent<EnemyBehavior>().EnemyOBJ;

        AttackTimer = EnemyOBJ.AttackRate;
        TurnSpeed = EnemyOBJ.MovementSpeed;

        ActiveState = State.ChooseAttack;

        OriginalRot = transform.rotation;
    }

    private void DoChooseAttack()
    {
        int AttackChoice = Random.Range(0, 2);
        Debug.Log(AttackChoice);

        if(AttackChoice == 0)
        {
            ActiveState = State.DirectAttack;
            StartCoroutine(SetTimer(AttackTimer));
        }
        else if(AttackChoice == 1)
        {
            ActiveState = State.SpinAttack;
            StartCoroutine(SetTimer(AttackTimer));
        }
    }

    private void DoDirectAttack()
    {
        Head.LookAt(Player);
        AttackPS.Play();

        if (IsAttacking == false)
        {
            AttackPS.Stop();
            ActiveState = State.ResetRotation;
        }
    }

    private void DoSpinAttack()
    {
        AttackPS.Play();

        if (IsAttacking == false)
        {
            AttackPS.Stop();
            ActiveState = State.ResetRotation;
        }
    }

    private void DoDead()
    {

    }

    private IEnumerator ResetRotation()
    {
        Head.rotation = OriginalRot;

        yield return null;

        ActiveState = State.ChooseAttack;
    }

    private IEnumerator SetTimer(float Duration)
    {
        float t = 0;
        IsAttacking = true;

        while(t < Duration)
        {
            t += Time.deltaTime;
            yield return null;
        }

        IsAttacking = false;
    }
}
