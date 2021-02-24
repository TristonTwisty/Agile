using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public void SaveGame()
    {

    }
}

[System.Serializable]
public class InventoryToken
{
    public float x;
    public float y;
    public float z;

    public float xor;
    public float yor;
    public float zor;

    [SerializeField] public List<ItemBase> ItemList_;

    public InventoryToken(Player ItemTemp)
    {

        ItemList_ = ItemTemp.Inventory;

        /*onboard_ = ItemTemp.onboard;
        Holdingbatt_ = ItemTemp.Holdingbatt;
        HoldingDisk_ = ItemTemp.HoldingDisk;
        HoldingWhip_ = ItemTemp.HoldingWhip;*/

        x = PlayerRefs.instance.checkpoint.position.x;
        y = PlayerRefs.instance.checkpoint.position.y;
        z = PlayerRefs.instance.checkpoint.position.z;

        xor = PlayerRefs.instance.checkpoint.rotation.x;
        yor = PlayerRefs.instance.checkpoint.rotation.y;
        zor = PlayerRefs.instance.checkpoint.rotation.z;
    }

}