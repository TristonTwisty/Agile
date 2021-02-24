using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiskItem : ItemBase
{
    private KeyCode SelectionKey = KeyCode.Alpha2;

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
