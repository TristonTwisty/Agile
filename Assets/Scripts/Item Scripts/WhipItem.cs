using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipItem : ItemBase
{
    /*public override void UseItem(GameObject source)
    {
        GameObject.Instantiate(VisualManager.instace.WhipVisual, source.transform.position,Quaternion.identity);
    }*/

    public override void ActivateObject(GameObject source)
    {
        VisualManager.instace.WhipVisual.SetActive(true);
    }

    public override void DeActivateObject(GameObject source)
    {
        VisualManager.instace.WhipVisual.SetActive(false);
    }
}
