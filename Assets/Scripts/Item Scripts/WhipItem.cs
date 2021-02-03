using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipItem : ItemBase
{
    public override void UseItem(Vector3 posistion)
    {
        GameObject.Instantiate(VisualManager.instace.WhipEffect,posistion,Quaternion.identity);
    }

}
