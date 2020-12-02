using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject CheckPoint = null;
    private Vector3 CheckPointPos;
    private Quaternion CheckPointRot;


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

    public GameObject escMenu;

    // My ugly code for UI
    [Header("UI")]
    private UIManager UIManager;
    

    //Added By Ricardo For UI
    //private Scriptforui scriptForUI;
    //public GameSounds gameSounds;
    //SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

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
        CheckPointPos = CheckPoint.transform.position;
        CheckPointRot = CheckPoint.transform.rotation;


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

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateMenu();
        }*/

    }

    /*public void ActivateMenu()
    {
        if (escMenu.activeInHierarchy == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerRefs.instance.movementS.enabled = false;
            PlayerRefs.instance.PlayerCamera.GetComponent<DomsMouseLook>().enabled = false;
            escMenu.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerRefs.instance.movementS.enabled = true;
            PlayerRefs.instance.PlayerCamera.GetComponent<DomsMouseLook>().enabled = true;
            escMenu.SetActive(false);
        }


    }*/

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

        // Turn on board UI
        UIManager.Hoverboard.enabled = true;

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

        //Turn off board UI
        UIManager.Hoverboard.enabled = false;

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

        UIManager.Shield.enabled = true;
        UIManager.SheildCpacity.enabled = true;
    }

    public void PickupDisk()
    {
        hasdisk = true;
        HoldingDisk = false;
        //SwitchToDisk();
    }

    public void PickupGun()
    {
        haswhip = true;
        HoldingWhip = false;
        //SwitchToGun();
    }

    public void PickupBatt()
    {
        hasBat = true;
        Holdingbatt = false;
        //SwitchToBatt();
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

            // Turn on disc UI
            UIManager.Disc.enabled = true;
        }
        else
        {
            PlayerRefs.instance.Disk.gameObject.SetActive(false);
            HoldingDisk = false;

            // Turn off disc UI
            UIManager.Disc.enabled = false;
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

            // Turn on hook UI
            UIManager.GrapplingHook.enabled = true;
        }
        else
        {
            PlayerRefs.instance.PlayerWhip.gameObject.SetActive(false);
            HoldingWhip = false;

            // Turn off hook UI
            UIManager.GrapplingHook.enabled = false;
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

            //Turn on Baton UI
            UIManager.Baton.enabled = true;
        } 
        else
        {
            PlayerRefs.instance.PlayerBatt.gameObject.SetActive(false);
            Holdingbatt = false;

            // Turn off Baton UI
            UIManager.Baton.enabled = false;
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log(CheckPoint + ("Updated to ") + CheckPoint);
        Debug.Log(CheckPointPos + ("Updated to ") + CheckPointPos);
        Debug.Log(CheckPointRot + ("Updated to ") + CheckPointRot);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        CheckPointPos.x = data.position[0];
        CheckPointPos.y = data.position[1];
        CheckPointPos.z = data.position[2];

        PlayerRefs.instance.Player.transform.position = CheckPointPos;
        PlayerRefs.instance.Player.transform.rotation = CheckPointRot;

        haswhip = data.haswhip;
        hasdisk = data.hasdisk;
        hasboard = data.hasboard;
        hasBat = data.hasBat;
        hasboots = data.hasboots;
        hasbelt = data.hasbelt;
        hasSheild = data.hasSheild;

    }
}
