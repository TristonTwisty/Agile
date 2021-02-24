using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SheildItem : ItemBase
{
    public override void ActivateObject(GameObject source)
    {
        VisualManager.instace.SheildVisual.SetActive(true);
    }

    public override void DeActivateObject(GameObject source)
    {
        VisualManager.instace.SheildVisual.SetActive(false);
    }
}
