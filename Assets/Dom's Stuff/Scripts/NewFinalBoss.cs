using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewFinalBoss : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private Transform Player;
    private Rigidbody PlayerRB;

    [Header("Components")]
    public Transform BossTarget;
    [HideInInspector] public EnemyBehavior EB;
    private EnemyScripableObject EnemyOBJ;
    private Rigidbody rigidbody;

    // States
    public enum State { Idle, ChooseAttack, Shield, Bat, Targeting, Dead }
    public State ActiveState = State.ChooseAttack;

    [Header("Stats")]
    private float Health;
    private float MovementSpeed;
    private bool IsAlive = true;
    public bool EndAttack = true;

    private void Awake()
    {
        Player = PlayerRefs.instance.Player;

        PlayerRB = Player.GetComponent<Rigidbody>();

        EB = GetComponent<EnemyBehavior>();
        EnemyOBJ = EB.EnemyOBJ;
        MovementSpeed = EnemyOBJ.MovementSpeed;

        rigidbody = GetComponent<Rigidbody>();

        TargetingClip = GetComponent<AudioSource>();

        ActiveState = State.Idle;
    }

    private IEnumerator Start()
    {
        while (IsAlive)
        {
            switch (ActiveState)
            {
                case State.Idle:
                    break;
                case State.ChooseAttack:
                    EndAttack = false;
                    ChooseAttack();
                    break;
                case State.Shield:
                    ShieldAttack();
                    break;
                case State.Bat:
                    BatAttack();
                    break;
                case State.Targeting:
                    TargetingAttack();
                    break;
                case State.Dead:
                    StartCoroutine(DoDead());
                    break;
            }
            yield return null;
        }
    }
    #region Attack Choice
    [Header("Bools")]
    private bool ShieldChosenLast, BatChosenLast, TargetChosenLast = false;
    private void ChooseAttack()
    {
        int AttackChoice = Random.Range(0, 3);
        //int AttackChoice = 2;

        switch (AttackChoice)
        {
            case 0:
                if (ShieldChosenLast)
                {
                    Debug.Log("Shield chosen last");
                }
                else
                {
                    ActiveState = State.Shield;
                    StartCoroutine(SetTimer(ShieldAttackDuration));
                    StartCoroutine(Movement(ShieldAttackLocation));
                    ShieldChosenLast = true;
                    BatChosenLast = TargetChosenLast = false;
                }
                break;
            case 1:
                if (BatChosenLast)
                {
                    Debug.Log("Bat chosen last");
                }
                else
                {
                    ActiveState = State.Bat;
                    StartCoroutine(SetTimer(BatAttackDuraton));
                    StartCoroutine(Movement(BatAttackLocation));
                    BatChosenLast = true;
                    ShieldChosenLast = TargetChosenLast = false;
                }
                break;
            case 2:
                if (TargetChosenLast)
                {
                    Debug.Log("Targeting chosen last");
                }
                else
                {
                    ActiveState = State.Targeting;
                    StartCoroutine(SetTimer(TargetAttackDuration));
                    StartCoroutine(Movement(TargetAttackLocation));
                    TargetingClip.Play();
                    TargetChosenLast = true;
                    BatChosenLast = ShieldChosenLast = false;
                }
                break;
        }
    }

    private IEnumerator Movement(Transform Location)
    {
        float t = 0;
        Vector3 startPosition = transform.position;
        while (t < MovementSpeed)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, Location.position, t / MovementSpeed);
            yield return null;
        }
    }

    private IEnumerator SetTimer(float Duration)
    {
        float t = 0;
        while (t < Duration)
        {
            t += Time.deltaTime;
            yield return null;
        }

        EndAttack = true;
    }
    #endregion
    #region ShieldAttack
    [Header("Shield Attack Components")]
    [SerializeField] private Transform ShieldAttackLocation;
    [SerializeField] private ParticleSystem ThunderStrike;
    [SerializeField] private Collider ShieldAttackCollider;
    public float ShieldAttackDamage = 1;
    [SerializeField] private float BatteryDropRadius = 16.5f;
    [SerializeField] private GameObject ShieldBattery;
    [SerializeField] private GameObject HealthPickup;

    [Header("Shield Attack Timers")]
    [SerializeField] private float ShieldAttackDuration;
    [SerializeField] private float ActivateColliderTimer = 2.5f;
    [SerializeField] private float ItemDropRate = 5;
    private float time = 0;

    private void ShieldAttack()
    {
        if (!ThunderStrike.isPlaying)
        {
            ThunderStrike.Play();
        }

        if (time < ItemDropRate)
        {
            time += Time.deltaTime;
        }
        else
        {
            ObjectPooling.Spawn(ShieldBattery, Random.insideUnitSphere * BatteryDropRadius + transform.position, Quaternion.identity);
            ObjectPooling.Spawn(HealthPickup, Random.insideUnitSphere * BatteryDropRadius + transform.position, Quaternion.identity);
            time = 0;
        }

        if (time >= ActivateColliderTimer)
        {
            ShieldAttackCollider.enabled = true;
        }

        if (EndAttack)
        {
            ShieldAttackCollider.enabled = false;
            ThunderStrike.Stop();
            ActiveState = State.ChooseAttack;
            time = 0;
        }
    }
    #endregion
    #region Bat Attack
    [Header("Bat Attack Comonents")]
    [SerializeField] private Transform BatAttackLocation;
    [SerializeField] private ParticleSystem SucitonPS;
    [SerializeField] private float SuctionPower = 1;
    public float PushForce;
    public float BatAttackDamage = 30;
    [SerializeField] private Collider BatAttackCollider;

    [Header("Bat Attack Timers")]
    [SerializeField] private float BatAttackDuraton = 30;
    [SerializeField] private float ActivateSuctionTimer = 2;
    private void BatAttack()
    {
        if(time < ActivateSuctionTimer)
        {
            time += Time.deltaTime;
        }
        if(time >= ActivateSuctionTimer)
        {
            PlayerRB.AddRelativeForce(-new Vector3(transform.position.x, 0, transform.position.z) * SuctionPower, ForceMode.Force);

            BatAttackCollider.enabled = true;

            if (!SucitonPS.isPlaying)
            {
                SucitonPS.Play();
            }
        }

        if (EndAttack)
        {
            SucitonPS.Stop();
            BatAttackCollider.enabled = false;
            ActiveState = State.ChooseAttack;
            time = 0;
        }
    }
    #endregion
    #region Targeting Attack
    [Header("Target Attack Components")]
    [SerializeField] private Transform TargetAttackLocation;
    [SerializeField] private Transform[] FireLocations;
    [SerializeField] private GameObject Missiles;
    public int MissilesReturned;
    private AudioSource TargetingClip;

    [Header("Target Attack Timers")]
    [SerializeField] private float TargetAttackDuration = 60;
    [SerializeField] private float MissileLaunchRate = 8;
    private void TargetingAttack()
    {
        if (!EndAttack)
        {
            if (time < MissileLaunchRate)
            {
                time += Time.deltaTime;
            }
            else
            {
                foreach (Transform FirePoint in FireLocations)
                {
                    ObjectPooling.Spawn(Missiles, FirePoint.position, Quaternion.identity);
                }
                time = 0;
            }
        }
        else
        {
            time = 0;
            MissilesReturned = 0;
            ActiveState = State.ChooseAttack;
        }
    }
    #endregion
    #region Death
    [Header("Death Handling")]
    [SerializeField] private Animator BlackoutAnimator;
    private IEnumerator DoDead()
    {
        IsAlive = false;

        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;

        BlackoutAnimator.SetTrigger("Fade out");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(0);
    }
    #endregion

    private void Update()
    {
        Health = EB.CurrentHealth;

        if (Health <= 0)
        {
            ActiveState = State.Dead;
        }

        if (ActiveState != State.Dead)
        {
            transform.LookAt(Player);
        }

        if (EB.ActivateBoss)
        {
            EB.ActivateBoss = false;
            ActiveState = State.ChooseAttack;
        }

        if(MissilesReturned >= 5)
        {
            EndAttack = true;
            MissilesReturned = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BatteryDropRadius);
    }
}