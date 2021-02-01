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
    [Header("Has")]
    public bool haswhip_;
    public bool hasdisk_;
    public bool hasboard_;
    public bool hasBat_;
    public bool hasboots_;
    public bool hasbelt_;
    public bool hasSheild_;

    [Header("In Use")]
    public bool onboard_;
    public bool Holdingbatt_;
    public bool HoldingDisk_;
    public bool HoldingWhip_;

    public float x;
    public float y;
    public float z;

    public InventoryToken(Inventory ItemTemp)
    {
        haswhip_ = ItemTemp.haswhip;
        hasdisk_ = ItemTemp.hasdisk;
        hasboard_ = ItemTemp.hasboard;
        hasBat_ = ItemTemp.hasBat;
        hasboots_ = ItemTemp.hasboots;
        hasbelt_ = ItemTemp.hasbelt;
        hasSheild_ = ItemTemp.hasSheild;


        onboard_ = ItemTemp.onboard;
        Holdingbatt_ = ItemTemp.Holdingbatt;
        HoldingDisk_ = ItemTemp.HoldingDisk;
        HoldingWhip_ = ItemTemp.HoldingWhip;

        x = PlayerRefs.instance.transform.position.x;
        y = PlayerRefs.instance.transform.position.y;
        z = PlayerRefs.instance.transform.position.z;
    }

}