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

    [Header("SpikeTraps")]
    [SerializeField] private GameObject[] ElectricTraps;
    private List<GameObject> SelectedTraps = new List<GameObject>();
    private int TrapCount = 0;

    [Header("Statistics")]
    [SerializeField ]private float DamageDealt;
    public float CurrentHealth;
    private float AttackTimer;
    private float TurnSpeed;
    private Quaternion OriginalRot;
    private float MaxHealth;

    // States
    [HideInInspector] public enum State {Initial, ChooseAttack, DirectAttack, SpinAttack, Dead}
    [HideInInspector] public State ActiveState = State.ChooseAttack;

    [Header("Bools")]
    private bool IsAlive = true;
    private bool IsAttacking = false;
    private bool ActivateTraps = false;
    public bool TestTraps = false;

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
        EnemyOBJ = GetComponent<EnemyBehavior>().EnemyOBJ;

        AttackTimer = EnemyOBJ.AttackRate;
        TurnSpeed = EnemyOBJ.MovementSpeed;

        ActiveState = State.ChooseAttack;

        OriginalRot = transform.rotation;

        MaxHealth = EnemyOBJ.Health;
        CurrentHealth = MaxHealth;
    }

    private void DoChooseAttack()
    {
        int AttackChoice = Random.Range(0, 2);

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
        Vector3 RelativePos = Player.position - transform.position;
        Quaternion Rotation = Quaternion.LookRotation(RelativePos, Vector3.up);

        Head.rotation = Quaternion.Slerp(Head.rotation, Rotation,  TurnSpeed * Time.deltaTime);

        AttackPS.Play();

        if (IsAttacking == false)
        {
            AttackPS.Stop();
            ActiveState = State.ChooseAttack;
        }
    }

    private void DoSpinAttack()
    {
        AttackPS.Play();

        Head.Rotate(Vector3.up * ( TurnSpeed * 40) * Time.deltaTime);

        if (IsAttacking == false)
        {
            AttackPS.Stop();
            ActiveState = State.ChooseAttack;
        }
    }

    private void DoDead()
    {

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

    private void Update()
    {
        if (CurrentHealth <= (CurrentHealth * .5) && ActivateTraps)
        {
            StartCoroutine(StartTraps(2));
        }
        else if (CurrentHealth <= (CurrentHealth * .25) && ActivateTraps)
        {
            StartCoroutine(StartTraps(3));
        }

        if (TestTraps && ActivateTraps)
        {
            StartCoroutine(StartTraps(4));
        }
    }

    private IEnumerator StartTraps(int SelectionNumber)
    {
        ActivateTraps = false;

        int RandomTrap = Random.Range(0, ElectricTraps.Length);

        Debug.Log("Trap # " + RandomTrap);

        while (TrapCount < SelectionNumber)
        {
            if (SelectedTraps.Contains(ElectricTraps[RandomTrap]))
            {
                RandomTrap = Random.Range(0, ElectricTraps.Length);
            }
            else
            {
                SelectedTraps.Add(ElectricTraps[RandomTrap]);
                TrapCount += 1;
            }
        }

        if (TrapCount == SelectionNumber)
        {
            foreach (GameObject Trap in SelectedTraps)
            {
                Trap.GetComponent<ElectricTrap>().ActivateTrap();
            }
        }

        yield return new WaitForSeconds(16);

        ActivateTraps = true;
    }
}