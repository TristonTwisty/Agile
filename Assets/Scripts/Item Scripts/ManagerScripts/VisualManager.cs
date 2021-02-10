using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    public static VisualManager instace;
    public GameObject WhipVisual;
    public GameObject DiskVisual;
    public GameObject BoardVisual;
    public GameObject BattVisual;
    public GameObject SheildVisual;
    public Component DashVisual;

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

}
