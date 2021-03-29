using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WhipItem : ItemBase
{
    [SerializeField] private KeyCode SelectionKey = KeyCode.Alpha3;
    public override GameObject ReturnGO()
    {
        return VisualManager.instace.WhipVisual.gameObject;
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
