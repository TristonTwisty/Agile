using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField] private Transform Player;


    [Header("Testing")]
    [SerializeField] private bool AITest = false;

    [Header("Components")]
    private EnemyBehavior EB;
    private EnemyScripableObject EnemyOBJ;

    // States
    private enum State { Initial, ChooseAttack, Disk, Whip, Shield, Bat, Targeting, Dead }
    private State ActiveState = State.Initial;

    [Header("Statistics")]
    [SerializeField] private float Radius;
    private bool IsAlive = true;
    private float Health;
    private float AttackTimer;

    [Header("Particals")]
    [SerializeField] private ParticleSystem Suction;
    [SerializeField] private ParticleSystem ThunderStrike;

    [Header("AttackPositions")]
    [SerializeField] private Transform DiskLocation;
    [SerializeField] private Transform WhipLocation;
    [SerializeField] private Transform ShieldLocation;
    [SerializeField] private Transform BatLocation;
    [SerializeField] private Transform TargetLocation;

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
                case State.Disk:
                    StartCoroutine(DoDisk());
                    break;
                case State.Whip:
                    StartCoroutine(DoWhip());
                    break;
                case State.Shield:
                    StartCoroutine(DoShield());
                    break;
                case State.Bat:
                    StartCoroutine(DoBat());
                    break;
                case State.Targeting:
                     StartCoroutine(DoTarget());
                    break;
                case State.Dead:
                     StartCoroutine(DoDead());
                    break;
            }
            yield return null;
        }
    }

    private void DoInitial()
    {
        gameObject.tag = "Boss";

        if (!AITest)
        {
            Player = PlayerRefs.instance.Player;
        }

        EB = GetComponent<EnemyBehavior>();
        EnemyOBJ = EB.EnemyOBJ;
        AttackTimer = EnemyOBJ.AttackRate;

        ActiveState = State.Shield;
    }

    private void DoChooseAttack()
    {
        int AttackChoice = Random.Range(0, 5);

        if (AttackChoice == 0)
        {
            ActiveState = State.Disk;
        }
        else if (AttackChoice == 1)
        {
            ActiveState = State.Whip;
        }
        else if (AttackChoice == 2)
        {
            ActiveState = State.Shield;
        }
        else if (AttackChoice == 3)
        {
            ActiveState = State.Bat;
        }
        else if (AttackChoice == 4)
        {
            ActiveState = State.Targeting;
        }
    }

    private IEnumerator DoDisk()
    {
        transform.position = Vector3.MoveTowards(transform.position, DiskLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(5);

        yield return new WaitForSeconds(AttackTimer);
    }

    private IEnumerator DoWhip()
    {
        yield return new WaitForSeconds(5);
    }

    private IEnumerator DoShield()
    {
        transform.position = Vector3.MoveTowards(transform.position, ShieldLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(5);

        ThunderStrike.Play();
        float Timer = 0;
        while (Timer < 5)
        {
            Timer += Time.deltaTime;
            yield return null;
        }
        Timer = 0;
        ObjectPooling.Spawn(Resources.Load<GameObject>("ItemPickUps/Health Pickup Small"), Random.insideUnitSphere * Radius + transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(AttackTimer);
    }

    private IEnumerator DoBat()
    {
        transform.position = Vector3.MoveTowards(transform.position, BatLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(5);
    }

    private IEnumerator DoTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(5);

        yield return new WaitForSeconds(AttackTimer);
    }

    private IEnumerator DoDead()
    {
        yield return new WaitForSeconds(5);

        yield return new WaitForSeconds(AttackTimer);
    }

    private void Update()
    {
        Health = EB.CurrentHealth;

        transform.LookAt(Player);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}