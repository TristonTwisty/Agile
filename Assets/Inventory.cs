using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public int Health;

    [Header("Has")]
    public bool haswhip;
    public bool hasdisk;
    public bool hasboard;
    public bool hasBat;
    public bool hasboots;
    public bool hasbelt;
    public bool hasSheild;

    [Header("In Use")]
    public bool onboard;
    public bool Holdingbatt;
    public bool HoldingDisk;
    public bool HoldingWhip;
    
    
    

    //Added By Ricardo For UI
    //private Scriptforui scriptForUI;
    //public GameSounds gameSounds;
    //SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        /*//finding all objects
        Player = GameObject.FindGameObjectWithTag("Player");
        RbPlayer = Player.GetComponent<Rigidbody>();
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        Iteminventory = GameObject.FindGameObjectWithTag("Inventory");
        GunPos = GameObject.Find("GunPickupPos");
        SheildPos = GameObject.Find("Sheild Pos");
        Batt = GameObject.Find("Batt");


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
        spriteRenderer = GetComponent<SpriteRenderer>();*/



    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1") && onboard == false && hasboard == true)
        {
            ActivateBoard();
        }
        else if (Input.GetKeyDown("1") && onboard == true && hasboard == true)
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
        onboard = true;
        PlayerRefs.instance.PlayerBoard.transform.position = PlayerRefs.instance.Player.transform.position;
        PlayerRefs.instance.PlayerBoard.transform.rotation = PlayerRefs.instance.Player.transform.rotation;
        PlayerRefs.instance.PlayerBoard.gameObject.SetActive(true);
        PlayerRefs.instance.movementS.enabled = false;
        PlayerRefs.instance.boxa.enabled = false;
        PlayerRefs.instance.boxb.enabled = false;
        PlayerRefs.instance.PlayerCamera.gameObject.SetActive(false);
    }

    private void ActivateBoard()
    {
        onboard = true;
        PlayerRefs.instance.PlayerBoard.transform.position = PlayerRefs.instance.Player.transform.position;
        PlayerRefs.instance.PlayerBoard.transform.rotation = PlayerRefs.instance.Player.transform.rotation;
        PlayerRefs.instance.PlayerBoard.gameObject.SetActive(true);
        PlayerRefs.instance.movementS.enabled = false;
        PlayerRefs.instance.boxa.enabled = false;
        PlayerRefs.instance.boxb.enabled = false;
        PlayerRefs.instance.PlayerCamera.gameObject.SetActive(false);

        PlayerRefs.instance.Disk.gameObject.SetActive(false);
        PlayerRefs.instance.PlayerWhip.gameObject.SetActive(false);
        PlayerRefs.instance.PlayerBatt.gameObject.SetActive(false);

    }

    private void DeActivateBoard()
    {
        onboard = false;
        PlayerRefs.instance.Player.transform.position = PlayerRefs.instance.PlayerBoard.transform.position;
        PlayerRefs.instance.Player.transform.rotation = PlayerRefs.instance.PlayerBoard.transform.rotation;
        PlayerRefs.instance.PlayerBoard.gameObject.SetActive(false);
        PlayerRefs.instance.movementS.enabled = true;
        PlayerRefs.instance.boxa.enabled = true;
        PlayerRefs.instance.boxb.enabled = true;
        PlayerRefs.instance.PlayerCamera.gameObject.SetActive(true);

        /*if (Holdingbatt)
        {
            SwitchToBatt();
        }
        else if (HoldingDisk)
        {
            SwitchToDisk();
        }
        else if (HoldingWhip)
        {
            SwitchToGun();
        }*/

    }

    public void PickUpBoard()
    {
        hasboard = true;
    }

    public void BootsPickedUp()
    {
        hasboots = true;
        PlayerRefs.instance.movementS.CanWallWalk = true;
    }

    public void PickupShield()
    {
        PlayerRefs.instance.Sheild.gameObject.SetActive(true);
        hasSheild = true;
    }

    public void PickupDisk()
    {
        hasdisk = true;
        HoldingDisk = false;
        SwitchToDisk();
    }

    public void PickupGun()
    {
        haswhip = true;
        HoldingWhip = false;
        SwitchToGun();
    }

    public void PickupBatt()
    {
        hasBat = true;
        Holdingbatt = false;
        SwitchToBatt();
    }

    private void SwitchToDisk()
    {
        if (HoldingDisk == false & hasdisk == true & onboard == false)
        {
            PlayerRefs.instance.PlayerWhip.gameObject.SetActive(false);
            PlayerRefs.instance.Disk.gameObject.SetActive(true);
            PlayerRefs.instance.PlayerBatt.gameObject.SetActive(false);
            PlayerRefs.instance.PlayerBoard.gameObject.SetActive(false);
            HoldingDisk = true;
            HoldingWhip = false;
            Holdingbatt = false;
        }
        else
        {
            PlayerRefs.instance.Disk.gameObject.SetActive(false);
            HoldingDisk = false;
        }
    }

    private void SwitchToGun()
    {
        if (HoldingWhip == false & haswhip == true & onboard == false)
        {
            PlayerRefs.instance.PlayerWhip.gameObject.SetActive(true);
            PlayerRefs.instance.Disk.gameObject.SetActive(false);
            PlayerRefs.instance.PlayerBatt.gameObject.SetActive(false);
            PlayerRefs.instance.PlayerBoard.gameObject.SetActive(false);
            HoldingWhip = true;
            HoldingDisk = false;
            Holdingbatt = false;
        }
        else
        {
            PlayerRefs.instance.PlayerWhip.gameObject.SetActive(false);
            HoldingWhip = false;
        }

    }

    private void SwitchToBatt()
    {
        if (Holdingbatt == false & hasBat == true & onboard == false)
        {
            PlayerRefs.instance.Disk.gameObject.SetActive(false);
            PlayerRefs.instance.PlayerWhip.gameObject.SetActive(false);
            PlayerRefs.instance.PlayerBatt.gameObject.SetActive(true);
            Holdingbatt = true;
            HoldingWhip = false;
            HoldingDisk = false;
        } 
        else
        {
            PlayerRefs.instance.PlayerBatt.gameObject.SetActive(false);
            Holdingbatt = false;
        }
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
        PlayerRefs.instance.Player.transform.position = position;

        hasboard = data.hasboard;
        hasdisk = data.hasdisk;
        haswhip = data.haswhip;

        

    }
}
