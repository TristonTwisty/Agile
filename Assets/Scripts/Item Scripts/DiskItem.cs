using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiskItem : ItemBase
{
    private KeyCode SelectionKey = KeyCode.Alpha2;


    //Added for UI 
    private Scriptforui scriptForUI;

    private void Start()
    {
        scriptForUI = Scriptforui.FindObjectOfType<Scriptforui>();
    }

    public override GameObject ReturnGO()
    {
        return VisualManager.instace.DiskVisual.gameObject;
    }


    public override bool PressSelectKey(KeyCode KeyPressed)
    {
        if (KeyPressed == SelectionKey)
        {
            return true;
        }
        return false;
    }

    public override void ActivateObject(GameObject source)
    {
        VisualManager.instace.DiskVisual.SetActive(true);
    }

    public override void DeActivateObject(GameObject source)
    {
        VisualManager.instace.DiskVisual.SetActive(false);
    }
}
