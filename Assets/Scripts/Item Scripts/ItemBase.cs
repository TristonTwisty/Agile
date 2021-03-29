using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBase
{
    public string ItemName;

    public virtual KeyCode returnKey()
    {
        return KeyCode.Delete;
    }

    public virtual void UseItem(GameObject source)
    {

    }

    public virtual void ActivateObject(GameObject source)
    {

    }

    public virtual void DeActivateObject(GameObject source)
    {

    }

    public virtual bool PressSelectKey(KeyCode KeyPressed)
    {
        return false;
    }

    public virtual GameObject ReturnGO()
    {
        return null;
    }
}
