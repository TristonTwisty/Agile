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
    private bool IsAlive = true;
    private float Health;
    //[HideInInspector] 
    public bool EndAttack = false;

    [Header("AttackPositions")]
    [SerializeField] private Transform DiskLocation;
    [SerializeField] private Transform WhipLocation;
    [SerializeField] private Transform ShieldLocation;
    [SerializeField] private Transform BatLocation;
    [SerializeField] private Transform TargetLocation;

    [Header("AttackBools")]
    private bool DiskChosenLast, WhipChosenLast, ShieldChosenLast, BatChosenLast, TargetChosenLast = false;

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
                    StartCoroutine(DoChooseAttack());
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
                     DoDead();
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

        ActiveState = State.Targeting;
    }

    private IEnumerator DoChooseAttack()
    {
        Debug.Log("Choosing attack");
        yield return new WaitForSeconds(5);

        int AttackChoice = Random.Range(0, 5);
        EndAttack = false;

        if (AttackChoice == 0)
        {
            if (DiskChosenLast)
            {
                DoChooseAttack();
            }
            else
            {
                ActiveState = State.Disk;
                DiskChosenLast = true;
                WhipChosenLast = ShieldChosenLast = BatChosenLast = TargetChosenLast = false;
            }
        }
        else if (AttackChoice == 1)
        {
            if (WhipChosenLast)
            {
                DoChooseAttack();
            }
            else
            {
                ActiveState = State.Whip;
                WhipChosenLast = true;
                DiskChosenLast = ShieldChosenLast = BatChosenLast = TargetChosenLast = false;
            }
        }
        else if (AttackChoice == 2)
        {
            if (ShieldChosenLast)
            {
                DoChooseAttack();
            }
            else
            {
                ActiveState = State.Shield;
                StartCoroutine(AttackTiming(EndShieldAttackTimer));
                ShieldChosenLast = true;
                DiskChosenLast = WhipChosenLast = BatChosenLast = TargetChosenLast = false;
            }
        }
        else if (AttackChoice == 3)
        {
            if (BatChosenLast)
            {
                DoChooseAttack();
            }
            else
            {
                ActiveState = State.Bat;
                StartCoroutine(AttackTiming(EndBatAttackTimer));
                BatChosenLast = true;
                DiskChosenLast = WhipChosenLast = ShieldChosenLast = TargetChosenLast = false;
            }
        }
        else if (AttackChoice == 4)
        {
            if (TargetChosenLast)
            {
                DoChooseAttack();
            }
            else
            {
                transform.rotation = Quaternion.identity;

                ActiveState = State.Targeting;
                StartCoroutine(AttackTiming(EndTargetingAttackTimer));
                TargetChosenLast = true;
                DiskChosenLast = WhipChosenLast = ShieldChosenLast = BatChosenLast = false;
            }
        }
    }

    private IEnumerator DoDisk()
    {
        transform.position = Vector3.MoveTowards(transform.position, DiskLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(5);

        Debug.Log("Do Disk Richochet Attack");

        yield return new WaitForSeconds(3);
    }

    private IEnumerator DoWhip()
    {
        transform.position = Vector3.MoveTowards(transform.position, WhipLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(5);

        Debug.Log("Do Whip Attack");

        yield return new WaitForSeconds(3);
    }

    #region Shield Attack

    [Header("Shield Attack Components")]
    public float ShieldAttackDamage;
    [SerializeField] private ParticleSystem ThunderStrike;
    [SerializeField] private Collider ThunderStrikeCollider;
    [SerializeField] private float BatteryDropRadius;

    [Header("Shield Atttack Timming")]
    [SerializeField] private float EnableColliderTimer = 5;
    [SerializeField] private float BatteryDropRate = 5;
    [SerializeField] private float ActivateThunderstrikeTimer = 5;
    [SerializeField] private float EndShieldAttackTimer = 60;

    private IEnumerator DoShield()
    {
        transform.position = Vector3.MoveTowards(transform.position, ShieldLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(ActivateThunderstrikeTimer);

        while (!EndAttack)
        {
            if (!ThunderStrike.isPlaying)
            {
                ThunderStrike.Play(true);
            }
            yield return new WaitForSeconds(EnableColliderTimer);
            
            ThunderStrikeCollider.enabled = true;
            ObjectPooling.Spawn(Resources.Load<GameObject>("ItemPickUps/Health Pickup Small"), Random.insideUnitSphere * BatteryDropRadius + transform.position, Quaternion.identity);
            yield return null;
        }

        ThunderStrike.Stop();
        ThunderStrikeCollider.enabled = false;
        ActiveState = State.ChooseAttack;
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
        transform.position = Vector3.MoveTowards(transform.position, BatLocation.position, EnemyOBJ.MovementSpeed);
        yield return new WaitForSeconds(ActivateBatAttackTimer);

        while (!EndAttack)
        {
            BatAttackCollider.enabled = true;
            if (!Suction.isPlaying)
            {
                Suction.Play();
            }
            Vector3 SuctionPosition = new Vector3(transform.position.x, 0, transform.position.z);
            Player.GetComponent<Rigidbody>().AddForce(SuctionPosition * SuctionPower, ForceMode.Impulse);
            yield return null;
        }

        Suction.Stop();
        BatAttackCollider.enabled = false;
        ActiveState = State.ChooseAttack;
    }

    #endregion

    #region Targeting Attack

    [Header("Targeting Attack Components")]
    [SerializeField] private ParticleSystem[] ThrusterFlame;
    [SerializeField] private GameObject[] Thrusters;
    [SerializeField] private float TurnSpeed = 150;
    private bool HasTriggered = false;

    [Header("Targeting Attack Timing")]
    [SerializeField] private float ThrusterActivationTimer = 5;
    [SerializeField] private float EndTargetingAttackTimer = 120;
    private IEnumerator DoTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetLocation.position, EnemyOBJ.MovementSpeed);

        yield return new WaitForSeconds(ThrusterActivationTimer);

        while (!EndAttack)
        {
            transform.Rotate(Vector3.up * TurnSpeed * Time.deltaTime);

            foreach (ParticleSystem PS in ThrusterFlame)
            {
                if (!PS.isPlaying)
                {
                    PS.Play();
                }
            }

            foreach (GameObject collider in Thrusters)
            {
                collider.GetComponent<Collider>().enabled = true;
                collider.tag = "Target";
            }
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
    }
    #endregion

    private void DoDead()
    {

    }

    private void Update()
    {
        Health = EB.CurrentHealth;

        if(ActiveState != State.Targeting)
        {
            transform.LookAt(Player);
        }

        if (HasTriggered == false)
        {
            if (ActiveState == State.Targeting)
            {
                bool IsEverythingActive = false;

                foreach (GameObject item in Thrusters)
                {
                    if (item.GetComponent<WeakPointAttack>().Activated == false)
                    {
                        IsEverythingActive = false;
                    }

                    if(HasTriggered == false && IsEverythingActive == true)
                    {
                        EndAttack = true;
                    }
                }
            }
        }
    }

    private IEnumerator AttackTiming(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);

        EndAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BatteryDropRadius);
    }
}