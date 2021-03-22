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

    //Added For Sounds
    [Header("UI")]
    private GameSounds gameSounds;
    private Scriptforui scriptForUI;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        //Added For Sounds
        gameSounds = GameSounds.FindObjectOfType<GameSounds>();

        //Added For UI
        scriptForUI = Scriptforui.FindObjectOfType<Scriptforui>();
    }

    private void Update()
    {
        if (HasDashBelt)
        {
            if (scriptForUI.currentDashAmount > 0)//Added For UI
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
        PlayerVelocity.z += DashPower;
        characterController.Move(PlayerVelocity);
        CurrentDashes -= 1;

        yield return new WaitForSeconds(DashRecharge);

        CurrentDashes += 1;
        PlayerVelocity.z = 0;
    }
}