using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBoss : MonoBehaviour
{
    [SerializeField] private Transform Player;

    [Header("Components")]
    [SerializeField] private EnemyScripableObject EnemyOBJ;
    [SerializeField] private ParticleSystem AttackPS;
    [SerializeField] private Transform Head;

    [Header("Statistics")]
    private float AttackTimer;
    public float DamageDealt = 10;
    private float TurnSpeed;

    // States
    [HideInInspector] public enum State {Initial, ChooseAttack, DirectAttack, SpinAttack, Dead }
    [HideInInspector] public State ActiveState = State.ChooseAttack;
    private bool IsAlive = true;

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
            }
            yield return null;
        }
    }

    private void DoInitial()
    {
        //Player = PlayerRefs.instance.Player;

        AttackTimer = EnemyOBJ.AttackRate;
        TurnSpeed = EnemyOBJ.MovementSpeed;

        ActiveState = State.ChooseAttack;
    }

    private void DoChooseAttack()
    {
        int AttackChoice = Random.Range(0, 1);

        if(AttackChoice == 0)
        {
            ActiveState = State.DirectAttack;
        }
        else if(AttackChoice == 1)
        {
            ActiveState = State.SpinAttack;
        }
    }

    private void DoDirectAttack()
    {
        float Timer = AttackTimer;
        Timer -= Time.deltaTime;

        Head.LookAt(Player);
        AttackPS.Play();

        if(Timer <= 0)
        {
            AttackPS.Stop();
            ActiveState = State.ChooseAttack;
        }
    }

    private void DoSpinAttack()
    {
        float Timer = AttackTimer;
        Timer -= Time.deltaTime;

        Head.RotateAround(transform.position, Head.up, 360 * TurnSpeed * Time.deltaTime);
        AttackPS.Play();

        if (Timer <= 0)
        {
            AttackPS.Stop();
            ActiveState = State.ChooseAttack;
        }
    }

    private void DoDead()
    {

    }
}
