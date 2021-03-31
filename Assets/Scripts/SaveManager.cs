using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryToken
{
    public float PlayerHealth_;

    public float x;
    public float y;
    public float z;

    public float xor;
    public float yor;
    public float zor;

    public ItemBase Item1;
    public ItemBase Item2;
    public ItemBase Item3;

    public List<ItemBase> ItemsInInventory;

    //public float PlayerHealth_;

    public InventoryToken(Player ItemTemp)
    {
        if (ItemTemp.Inventory.Count >= 1)
        {
            Debug.Log("Made it to 1");
            Item1 = ItemTemp._inventory[0];
            Debug.Log(Item1);
        }
        if (ItemTemp.Inventory.Count >= 2)
        {
            Debug.Log("Made it to 2");
            Item2 = ItemTemp._inventory[1];
            Debug.Log(Item2);
        }
        if (ItemTemp.Inventory.Count >= 3)
        {
            Debug.Log("Made it to 3");
            Item3 = ItemTemp._inventory[2];
            Debug.Log(Item3);
        }

        ItemsInInventory = ItemTemp._inventory;

        /*for (int i = 0; i < ItemsInInventory.Count; i++)
        {
            Debug.Log(ItemsInInventory[i]);
        }*/


        PlayerHealth_ = PlayerRefs.instance.currentHealth;
        Scriptforui.instance.playerHealthSlider.value = PlayerRefs.instance.currentHealth;

        x = PlayerRefs.instance.checkpoint.position.x;
        y = PlayerRefs.instance.checkpoint.position.y;
        z = PlayerRefs.instance.checkpoint.position.z;
        //Debug.Log(x);
        //Debug.Log(y);
        //Debug.Log(z);

        xor = PlayerRefs.instance.checkpoint.rotation.x;
        yor = PlayerRefs.instance.checkpoint.rotation.y;
        zor = PlayerRefs.instance.checkpoint.rotation.z;
    }

}