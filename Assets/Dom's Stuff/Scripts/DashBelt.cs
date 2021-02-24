using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBelt : MonoBehaviour
{
    [SerializeField] private float DashPower = 20;
    public int MaximumDashes = 3;
    //[HideInInspector] 
    public int CurrentDashes;
    [SerializeField] private float DashRecharge = 5;
    public bool HasDashBelt;

    private Rigidbody RB;

    //Added For Sounds
    private GameSounds gameSounds;
    private Scriptforui scriptForUI;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();

        CurrentDashes = MaximumDashes;

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

<<<<<<< HEAD
                        //Added For Sounds and UI
                        gameSounds.audioSource.PlayOneShot(gameSounds.playerDash);
                        scriptForUI.currentDashAmount = scriptForUI.currentDashAmount - 1;
                        scriptForUI.displayTotalDashAmount.text = scriptForUI.currentDashAmount.ToString();

                    }
=======
                    //Added For Sounds
                    //gameSounds.audioSource.PlayOneShot(gameSounds.playerDash);
>>>>>>> 2b1f738b95ea290543c036b108ba4dd1f188db88
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
        RB.AddForce(transform.forward * DashPower, ForceMode.Impulse);
        CurrentDashes -= 1;

        yield return new WaitForSeconds(DashRecharge);

        CurrentDashes += 1;
    }
}