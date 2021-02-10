using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattItem : ItemBase
{
    public override void ActivateObject(GameObject source)
    {
        VisualManager.instace.BattVisual.SetActive(true);
    }

    public override void DeActivateObject(GameObject source)
    {
        VisualManager.instace.BattVisual.SetActive(false);
    }
}
