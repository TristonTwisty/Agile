using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashItem : ItemBase
{
    public override void ActivateObject(GameObject source)
    {
        VisualManager.instace.DashVisual.GetComponent<DashBelt>().enabled = true;
    }

    public override void DeActivateObject(GameObject source)
    {
        //VisualManager.instace.DashVisual.GetComponent<DashBelt>().enabled = false;
    }
}
