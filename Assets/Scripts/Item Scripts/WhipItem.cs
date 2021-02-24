using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipItem : ItemBase
{
    /*public override void UseItem(GameObject source)
    {
        GameObject.Instantiate(VisualManager.instace.WhipVisual, source.transform.position,Quaternion.identity);
    }*/

    private KeyCode SelectionKey = KeyCode.Alpha4;

    //Added for UI 
    private Scriptforui scriptForUI;

    private void Start()
    {
        scriptForUI = Scriptforui.FindObjectOfType<Scriptforui>();
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
        VisualManager.instace.WhipVisual.SetActive(true);
    }

    public override void DeActivateObject(GameObject source)
    {
        VisualManager.instace.WhipVisual.SetActive(false);
    }
}
