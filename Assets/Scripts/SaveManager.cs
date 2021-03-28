using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryToken
{
    public float x;
    public float y;
    public float z;

    public float xor;
    public float yor;
    public float zor;

    public ItemBase Item0 = null;
    public ItemBase Item1 = null;
    public ItemBase Item2 = null;

    //public float PlayerHealth_;

    public InventoryToken(Player ItemTemp)
    {
        if (ItemTemp.Inventory.Count >= 1)
        {
            Debug.Log("Made it to 0");
            Item0 = ItemTemp._inventory[0];
            Debug.Log(Item0);
        }

        
        

        //PlayerHealth_ = PlayerRefs.instance.PlayerHealth;

        /*x = PlayerRefs.instance.checkpoint.position.x;
        y = PlayerRefs.instance.checkpoint.position.y;
        z = PlayerRefs.instance.checkpoint.position.z;

        xor = PlayerRefs.instance.checkpoint.rotation.x;
        yor = PlayerRefs.instance.checkpoint.rotation.y;
        zor = PlayerRefs.instance.checkpoint.rotation.z;*/
    }

}