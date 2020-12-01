using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player;
    public Rigidbody RbPlayer;
    public GameObject PlayerCamera;
    public GameObject GunPos;
    public GameObject SheildPos;
    public int Health = 100;
    public string LastHeld;

    [Header("Items")]
    public GameObject Board;
    public GameObject Disk;
    public GameObject Whip;
    public GameObject Sheild;
    private GameObject Iteminventory;

    [Header("Board Settings")]
    public Rigidbody BoardRb;
    public Component BoardMovement;
    public Component BoardInput;
    public GameObject BoardPerf;
    public GameObject BoardCamera;
    public bool onboard;
    private Collider BoardPerfCol;

    [Header("Disk Settings")]
    public GameObject DiskHolster;
    public Component DiskTossScript;

    [Header("Whip Settings")]
    public Component WhipRotationScript;
    public Component GrapplingScript;

    [Header("Bat Settings")]
    public GameObject Batt;

    [Header("Item Bools")]
    public bool hasboard;
    public bool hasdisk;
    public bool haswhip;
    public bool hasBat;

    //Added By Ricardo For UI
    private Scriptforui scriptForUI;
    public GameSounds gameSounds;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //finding all objects
        Player = GameObject.FindGameObjectWithTag("Player");
        RbPlayer = Player.GetComponent<Rigidbody>();
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        Iteminventory = GameObject.FindGameObjectWithTag("Inventory");
        GunPos = GameObject.Find("GunPickupPos");
        SheildPos = GameObject.Find("Sheild Pos");
        Batt = GameObject.FindGameObjectWithTag("Batt");


        //finding all Board things needed to manage
        Board = GameObject.FindGameObjectWithTag("Hoverboard");
        BoardRb = Board.GetComponent<Rigidbody>();
        BoardMovement = Board.GetComponent<HoverboardMovement>();
        BoardInput = Board.GetComponent<HoverboardInput>();
        BoardPerf = GameObject.Find("Board_Pref");
        BoardPerfCol = BoardPerf.GetComponent<Collider>();
        BoardCamera = GameObject.FindGameObjectWithTag("Board Camera");

        //finding all Disk things needed to manage
        Disk = GameObject.FindGameObjectWithTag("Particle Disc");
        DiskHolster = GameObject.Find("Ring Holster");
        DiskTossScript = Disk.GetComponent<NewRingToss>();


        //finding all Whip things needed to manage
        Whip = GameObject.FindGameObjectWithTag("Whip");
        WhipRotationScript = Whip.GetComponent<RotateGun>();
        GrapplingScript = GameObject.Find("Whip").GetComponent<Grappling>();


        //Added By Ricardo For UI
        scriptForUI = GameObject.FindObjectOfType<Scriptforui>();
        gameSounds = GameObject.FindObjectOfType<GameSounds>();
        spriteRenderer = GetComponent<SpriteRenderer>();



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1") && onboard == false)
        {
            ActivateBoard();
        }
        else if (Input.GetKeyDown("1") && onboard == true)
        {
            DeActivateBoard();
        }

        if (Input.GetKeyDown("2") && hasdisk == true && onboard == false)
        {
            SwitchToDisk();
        }

        if (Input.GetKeyDown("3") && haswhip == true && onboard == false)
        {
            SwitchToGun();
        }

        if (Input.GetKeyDown("4") && hasBat == true && onboard == false)
        {
            SwitchToBatt();
        }

    }


    public void OnTriggerEnterBoardFunction()
    {
        hasboard = true;
        ActivateBoard();
        //Added By Ricardo
        //gameSounds.audioSource.PlayOneShot(gameSounds.hoverBoardStationary);

        //added by ricardo for UI
        if (haswhip == true)
        {
            //Added By Ricardo
            scriptForUI.thirdItem.sprite = scriptForUI.playerWhip.sprite;
        }
        if (hasdisk == true)
        {
            //Added By Ricardo
            scriptForUI.secondItem.sprite = scriptForUI.playerDisc.sprite;
        }
    }

    private void ActivateBoard()
    {
        if (hasboard == true) {
        //get On board
        Board.transform.parent = null;
        Board.gameObject.SetActive(true);
        BoardInput.GetComponent<HoverboardInput>().enabled = true;
        BoardMovement.GetComponent<HoverboardMovement>().enabled = true;
        Player.gameObject.SetActive(false);
        Player.transform.parent = Board.transform;
        Player.transform.localPosition = new Vector3(0, 0, 0);
        onboard = true;

        //Added By Ricardo For UI
        gameSounds.audioSource.PlayOneShot(gameSounds.hoverBoardStationary);
        scriptForUI.currentItem.sprite = scriptForUI.playerHoverBoard.sprite;
        scriptForUI.itemInSlot1 = true;
        //End Ricardo
     
        if (haswhip == true)
            {
                Whip.gameObject.SetActive(false);
                //Added By Ricardo
                scriptForUI.thirdItem.sprite = scriptForUI.playerWhip.sprite;
            }
        if (hasdisk == true)
            {
                Disk.gameObject.SetActive(false);
                //Added By Ricardo
                scriptForUI.secondItem.sprite = scriptForUI.playerDisc.sprite;
            }
        if (hasBat == true)
            {
                Batt.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("you no have board dummy");
        }
    }

    private void DeActivateBoard()
    {
        if (hasboard == true) {
        //get Off board
        Player.transform.parent = null;
        Player.gameObject.SetActive(true);
        Board.gameObject.SetActive(false);
        BoardInput.GetComponent<HoverboardInput>().enabled = false;
        BoardMovement.GetComponent<HoverboardMovement>().enabled = false;
        Board.transform.parent = Player.transform;
        Board.transform.localPosition = new Vector3(0, 0, 0);
        onboard = false;

         //added by Ricardo
        gameSounds.audioSource.Stop();

        if (LastHeld == "Disk")
            {
                SwitchToDisk();
            }
        else if (LastHeld == "Whip")
            {
                SwitchToGun();
            }
        else if (LastHeld == "Batt")
            {
                SwitchToBatt();
            }
        }
        
        else
        {
            Debug.Log("you no have board dummy");
        }
    }

    public void PickupRing()
    {
        Debug.Log("ring pickup called");
        DiskTossScript.GetComponent<NewRingToss>().enabled = true;
        DiskTossScript.GetComponent<Pickup>().enabled = false;
        hasdisk = true;
        LastHeld = "Disk";

        //Added By Ricardo For UI
        scriptForUI.currentItem.sprite = scriptForUI.playerDisc.sprite;
        scriptForUI.itemInSlot1 = true;
      

        if (Whip.gameObject.activeInHierarchy == true && haswhip == true)
        {
            Whip.gameObject.SetActive(false);
        }

        if (haswhip == true)
        {
            //Added By Ricardo
            scriptForUI.thirdItem.sprite = scriptForUI.playerWhip.sprite;
        }
        if (hasboard == true)
        {
            //Added By Ricardo
            scriptForUI.secondItem.sprite = scriptForUI.playerHoverBoard.sprite;

        }
        if (hasBat == true)
        {
            Batt.gameObject.SetActive(false);
        }
        ///End Ricardo
    }

    private void SwitchToDisk()
    {
        if (haswhip == true)
        {
            Whip.gameObject.SetActive(false);
            //Added By Ricardo
            scriptForUI.thirdItem.sprite = scriptForUI.playerWhip.sprite;
        }
        if (hasdisk == true)
        {
            scriptForUI.secondItem.sprite = scriptForUI.playerWhip.sprite;
            //Added By Ricardo For UI
            scriptForUI.currentItem.sprite = scriptForUI.playerDisc.sprite;
            scriptForUI.itemInSlot1 = true;
        }
        if (hasboard == true)
        {
            Board.gameObject.SetActive(false);
            //Added By Ricardo
            scriptForUI.secondItem.sprite = scriptForUI.playerHoverBoard.sprite;

        }

        if (Disk.gameObject.activeInHierarchy == false)
        {
            Disk.gameObject.SetActive(true);
        }

        if (hasBat == true)
        {
            Batt.gameObject.SetActive(false);
        }

        LastHeld = "Disk";
    }

    public void GunPickup()
    {
        Whip.transform.parent = Player.transform;
        Whip.transform.position = GunPos.transform.position;
        Whip.transform.parent = PlayerCamera.transform;
        GrapplingScript.GetComponent<Grappling>().enabled = true;
        WhipRotationScript.GetComponent<RotateGun>().enabled = true;
        Whip.GetComponent<Pickup>().enabled = false;
        GunPos.gameObject.SetActive(false);
        haswhip = true;
        LastHeld = "Whip";

 

        if (Disk.gameObject.activeInHierarchy == true && hasdisk == true)
        {
            Disk.gameObject.SetActive(false);
        }

        //Added By Ricardo For UI
        scriptForUI.currentItem.sprite = scriptForUI.playerWhip.sprite;
        scriptForUI.itemInSlot1 = true;

        if (hasdisk == true)
        {
            //Added By Ricardo
            scriptForUI.secondItem.sprite = scriptForUI.playerDisc.sprite;
        }
        if (hasboard == true)
        {
            //Added By Ricardo
            scriptForUI.thirdItem.sprite = scriptForUI.playerHoverBoard.sprite;
        }
        if (hasBat == true)
        {
            Batt.gameObject.SetActive(false);
        }

        //End Ricardo
    }

    private void SwitchToGun()
    {

        //Added By Ricardo For UI
        scriptForUI.currentItem.sprite = scriptForUI.playerWhip.sprite;
        scriptForUI.itemInSlot1 = true;
        //End Ricardo

        if (hasdisk == true)
        {
            Disk.gameObject.SetActive(false);
            //Added By Ricardo
            scriptForUI.secondItem.sprite = scriptForUI.playerDisc.sprite;

        }

        if (hasboard == true)
        {
            Board.gameObject.SetActive(false);
            //Added By Ricardo
            scriptForUI.thirdItem.sprite = scriptForUI.playerHoverBoard.sprite;
        }

        if (Whip.gameObject.activeInHierarchy == false)
        {
            Whip.gameObject.SetActive(true);
        }

        if (hasBat == true)
        {
            Batt.gameObject.SetActive(false);
        }

        LastHeld = "Whip";

        //Added By Ricardo For UI
        scriptForUI.currentItem.sprite = scriptForUI.playerWhip.sprite;
        scriptForUI.itemInSlot1 = true;
    }

    public void PickupShield()
    {
        Sheild.gameObject.SetActive(true);
        Sheild.transform.parent = null;
        Sheild.transform.parent = PlayerCamera.transform;
        Sheild.transform.position = SheildPos.transform.position;
        Sheild.transform.rotation = SheildPos.transform.rotation;
        Sheild.GetComponent<ParticleShield>().enabled = true;

        //added by ricardo for ui
        scriptForUI.shieldImage.gameObject.SetActive(true);
        scriptForUI.shieldRechargeSlider.gameObject.SetActive(true);
    }

    public void PickUpBoots()
    {
        Player.GetComponent<WallWalker>().CanWallWalk = true;
    }

    public void PickupBatt()
    {
        Batt.gameObject.SetActive(true);
        hasBat = true;

        //NEEDS Updated Ricky
        //scriptForUI.shieldImage.gameObject.SetActive(true);
        //scriptForUI.shieldRechargeSlider.gameObject.SetActive(true);
    }

    public void SwitchToBatt()
    {
        if (hasdisk == true)
        {
            Disk.gameObject.SetActive(false);
            //NEEDS UPDATE RICKY
            //scriptForUI.secondItem.sprite = scriptForUI.playerDisc.sprite;

        }

        if (hasboard == true)
        {
            Board.gameObject.SetActive(false);
            //NEEDS UPDATE RICKY
            //scriptForUI.thirdItem.sprite = scriptForUI.playerHoverBoard.sprite;
        }

        if (haswhip == true)
        {
            Whip.gameObject.SetActive(false);
        }

        if (Batt.activeInHierarchy == false)
        {
            Batt.gameObject.SetActive(true);
        }

        LastHeld = "Batt";

        //NEEDS UPDATE RICKY
        //scriptForUI.currentItem.sprite = scriptForUI.playerWhip.sprite;
        //scriptForUI.itemInSlot1 = true;
    }

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        Player.transform.position = position;

        hasboard = data.hasboard;
        hasdisk = data.hasdisk;
        haswhip = data.haswhip;

        

    }
}
