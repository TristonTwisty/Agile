using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private Transform Player;
    private Rigidbody PlayerRB;

    [Header("Testing")]
    [SerializeField] private bool AITest = false;

    [Header("Components")]
    [HideInInspector] public EnemyBehavior EB;
    private EnemyScripableObject EnemyOBJ;

    // States
    private enum State { ChooseAttack, Shield, Bat, Targeting, Dead }
    private State ActiveState = State.ChooseAttack;

    [Header("Statistics")]
    private float Health;
    [HideInInspector] public bool EndAttack = true;
    private float MovementSpeed;

    private void Start()
    {
        if (!AITest)
        {
            Player = PlayerRefs.instance.Player;
        }
        PlayerRB = Player.GetComponent<Rigidbody>();

        EB = GetComponent<EnemyBehavior>();
        EnemyOBJ = EB.EnemyOBJ;
        MovementSpeed = EnemyOBJ.MovementSpeed;
    }

    private void CheckState()
    {
        switch (ActiveState)
        {
            case State.ChooseAttack:
                StartCoroutine(DoChooseAttack());
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
                DoDead();
                break;
        }
    }

    #region Choose Attack
    [Header("Choose Next Attack")]
    [SerializeField] private float AttackCooldown = 5;
    [SerializeField] private Transform ShieldLocation;
    [SerializeField] private Transform BatLocation;
    [SerializeField] private Transform TargetLocation;
    private bool ShieldChosenLast, BatChosenLast, TargetChosenLast = false;
    private IEnumerator DoChooseAttack()
    {
        Debug.Log("Choosing attack");
        yield return new WaitForSeconds(AttackCooldown);

        int AttackChoice = Random.Range(0, 3);
        
        if (AttackChoice == 0)
        {
            if (ShieldChosenLast)
            {
                CheckState();
            }
            else
            {
                ActiveState = State.Shield;
                CheckState();
                ShieldChosenLast = true;
                BatChosenLast = TargetChosenLast = false;
            }
        }
        else if (AttackChoice == 1)
        {
            if (BatChosenLast)
            {
                CheckState();
            }
            else
            {
                ActiveState = State.Bat;
                CheckState();
                BatChosenLast = true;
                ShieldChosenLast = TargetChosenLast = false;
            }
        }
        else if (AttackChoice == 2)
        {
            if (TargetChosenLast)
            {
                CheckState();
            }
            else
            {
                transform.rotation = Quaternion.identity;

                ActiveState = State.Targeting;
                CheckState();
                TargetChosenLast = true;
                ShieldChosenLast = BatChosenLast = false;
            }
        }
    }
    #endregion

    #region Shield Attack

    [Header("Shield Attack Components")]
    public float ShieldAttackDamage;
    [SerializeField] private ParticleSystem ThunderStrike;
    [SerializeField] private Collider ThunderStrikeCollider;
    [SerializeField] private float BatteryDropRadius;
    [SerializeField] private GameObject ShieldBattery;

    [Header("Shield Atttack Timming")]
    [SerializeField] private float EnableColliderTimer = 5;
    [SerializeField] private float BatteryDropRate = 5;
    [SerializeField] private float EndShieldAttackTimer = 60;

    private IEnumerator DoShield()
    {
        Debug.Log("Shield Attack");
        EndAttack = false;
        float t = 0;
        Vector3 startPosition = transform.position;
        while (t < MovementSpeed)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, ShieldLocation.position, t/MovementSpeed);
            yield return null;
        }
        ThunderStrike.Play();
        yield return new WaitForSeconds(EnableColliderTimer);

        StartCoroutine(AutoEndAttack(EndShieldAttackTimer));
        ThunderStrikeCollider.enabled = true;

        while (!EndAttack)
        {
            yield return new WaitForSeconds(BatteryDropRate);
            ObjectPooling.Spawn(ShieldBattery, Random.insideUnitSphere * BatteryDropRadius + transform.position, Quaternion.identity);
            yield return null;
        }

        ActiveState = State.ChooseAttack;
        CheckState();
        ThunderStrike.Stop();
        ThunderStrikeCollider.enabled = false;
    }

    #endregion

    #region Bat Attack

    [Header("Bat Attack Components")]
    [SerializeField] private ParticleSystem Suction;
    [SerializeField] private Collider BatAttackCollider;
    [SerializeField] private float SuctionPower = 2;
    public float BatAttackDamage = 10;

    [Header("Bat Attack Timing")]
    [SerializeField] private float ActivateBatAttackTimer = 5;
    [SerializeField] private float EndBatAttackTimer = 120;

    private IEnumerator DoBat()
    {
        Debug.Log("Bat attack");
        EndAttack = false;
        float t = 0;
        Vector3 startPosition = transform.position;
        while (t < MovementSpeed)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, BatLocation.position,  t/MovementSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(ActivateBatAttackTimer);

        StartCoroutine(AutoEndAttack(EndBatAttackTimer));
        Suction.Play();
        BatAttackCollider.enabled = true;

        while (!EndAttack)
        {
            PlayerRB.AddForce(new Vector3(transform.position.x, 0, transform.position.z) * SuctionPower, ForceMode.Force);
            yield return null;
        }

        Debug.Log("Ending");
        ActiveState = State.ChooseAttack;
        CheckState();
        Suction.Stop();
        BatAttackCollider.enabled = false;
    }

    #endregion

    #region Targeting Attack

    [Header("Targeting Attack Components")]
    [SerializeField] private ParticleSystem[] ThrusterFlame;
    [SerializeField] private GameObject[] Thrusters;
    [SerializeField] private float TurnSpeed = 150;
    private bool HasTriggered = false;

    [Header("Targeting Attack Timing")]
    [SerializeField] private float ActivateThrustersTimer = 5;
    [SerializeField] private float EndTargetingAttackTimer = 120;
    private IEnumerator DoTarget()
    {
        Debug.Log("Targeting attack");
        EndAttack = false;
        float t = 0;
        Vector3 startPosition = transform.position;
        while (t < MovementSpeed)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, TargetLocation.position, t/MovementSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(ActivateThrustersTimer);

        foreach (ParticleSystem PS in ThrusterFlame)
        {
            PS.Play();
        }
        foreach (GameObject collider in Thrusters)
        {
            collider.GetComponent<Collider>().enabled = false;
            collider.tag = null;
        }

        while (!EndAttack)
        {
            transform.Rotate(Vector3.up * (TurnSpeed * Time.deltaTime));
            yield return null;
        }

        foreach (ParticleSystem PS in ThrusterFlame)
        {
            PS.Stop();
        }
        foreach (GameObject collider in Thrusters)
        {
            collider.GetComponent<Collider>().enabled = false;
            collider.tag = null;
        }

        ActiveState = State.ChooseAttack;
        CheckState();
    }
    #endregion

    private void DoDead()
    {

    }

    private void Update()
    {
        Health = EB.CurrentHealth;

        if(Health <= 0)
        {
            ActiveState = State.Dead;
            CheckState();
        }

        if(ActiveState != State.Targeting)
        {
            transform.LookAt(Player);
        }

        if (ActiveState == State.Targeting)
        {
            if (HasTriggered == false)
            {
                bool IsEverythingActive = true;

                foreach (GameObject item in Thrusters)
                {
                    if (item.GetComponent<WeakPointAttack>().Activated == false)
                    {
                        IsEverythingActive = false;
                    }

                    if(HasTriggered == false && IsEverythingActive == true)
                    {
                        EndAttack = true;
                        EB.TakeDamage(1);
                    }
                }
            }
        }

        if (EB.ActivateBoss)
        {
            EB.ActivateBoss = false;
            ActiveState = State.ChooseAttack;
            CheckState();
        }
    }

    private IEnumerator AutoEndAttack(float WaitTime)
    {
        float t = 0;
        while (!EndAttack)
        {
            t += Time.deltaTime;

            if(t >= WaitTime)
            {
                EndAttack = true;
            }
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BatteryDropRadius);
    }
}