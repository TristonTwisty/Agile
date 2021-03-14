using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    public static VisualManager instace;
    public static PlayerRefs refs;
    public GameObject WhipVisual;
    public GameObject DiskVisual;
    public GameObject BoardVisual;
    public GameObject BattVisual;
    public GameObject SheildVisual;
    public Component DashVisual;

    public GameObject PlayerSurface;
    public GameObject PlayerJoints;


    private void Awake()
    {
        if(VisualManager.instace == null)
        {
            VisualManager.instace = this;
        }
        else if (VisualManager.instace != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        WhipVisual = PlayerRefs.instance.PlayerWhip.gameObject;
        DiskVisual = PlayerRefs.instance.Disk.gameObject;
        BoardVisual = PlayerRefs.instance.PlayerBoard.gameObject;
        BattVisual = PlayerRefs.instance.Batt.gameObject;
        SheildVisual = PlayerRefs.instance.Sheild.gameObject;
    }

}
