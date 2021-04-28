using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBelt : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float DashPower = 20;
    public int MaximumDashes = 3;
    public int CurrentDashes;
    [SerializeField] private float DashRecharge = 5;
    public bool HasDashBelt;
    private Vector3 PlayerVelocity;

    [Header("Character Controller")]
    private CharacterController characterController;
    private Rigidbody rigidbody;

    //Added For Sounds
    [Header("UI")]
    [SerializeField] private bool DisableUI;
    private GameSounds gameSounds;
    private Scriptforui scriptForUI;

    [Header("Which character controller?")]
    [SerializeField] private bool HasCharacterController;
    [SerializeField] private bool HasRigibody;


    //Added by Ricardo
 

    private void Start()
    {
        if(GetComponent<CharacterController>() != null)
        {
            HasCharacterController = true;
            HasRigibody = false;
            characterController = GetComponent<CharacterController>();
        }
        else if(GetComponent<Rigidbody>() != null)
        {
            HasRigibody = true;
            HasCharacterController = false;
            rigidbody = GetComponent<Rigidbody>();
        }

        //Added For Sounds
        gameSounds = GameSounds.FindObjectOfType<GameSounds>();

        //Added For UI
        scriptForUI = Scriptforui.FindObjectOfType<Scriptforui>();

        CurrentDashes = MaximumDashes;
    }

    private void Update()
    {
        if (HasDashBelt)
        {
            if (DisableUI)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && CurrentDashes > 0)
                {
                    StartCoroutine(Dash());
                }
            }
            else if (scriptForUI.currentDashAmount > 0)//Added For UI
            {
                if (CurrentDashes > 0)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        StartCoroutine(Dash());

                        //Added For Sounds and UI
                        //gameSounds.audioSource.PlayOneShot(gameSounds.playerDash);
                        scriptForUI.currentDashAmount = scriptForUI.currentDashAmount - 1;
                        scriptForUI.displayTotalDashAmount.text = scriptForUI.currentDashAmount.ToString();
                    }
                }
            }
        }

        if(CurrentDashes < 0)
        {
            CurrentDashes = 0;
        }

        if(CurrentDashes > MaximumDashes)
        {
            CurrentDashes = MaximumDashes;
        }
    }

    private IEnumerator Dash()
    {
        if (HasCharacterController)
        {
            characterController.Move(transform.forward * DashPower);
        }
        else if (HasRigibody)
        {
            rigidbody.AddForce(transform.forward * DashPower, ForceMode.VelocityChange);
        }
        CurrentDashes -= 1;

        yield return new WaitForSeconds(DashRecharge);

        CurrentDashes += 1;
        PlayerVelocity.z = 0;
    }
}