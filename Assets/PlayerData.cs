using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public float[] position;

    public bool haswhip;
    public bool hasdisk;
    public bool hasboard;
    public bool hasBat;
    public bool hasboots;
    public bool hasbelt;
    public bool hasSheild;

    public string BoardParent;
    public string DiskParent;
    public string WhipParent;

    public PlayerData (Inventory Player)
    {
        position = new float[3];
        position[0] = Player.CheckPoint.transform.position.x;
        position[1] = Player.CheckPoint.transform.position.y;
        position[2] = Player.CheckPoint.transform.position.z;

        haswhip = Player.haswhip;
        hasdisk = Player.hasdisk;
        hasboard = Player.hasboard;
        hasBat = Player.hasBat;
        hasboots = Player.hasboots;
        hasbelt = Player.hasbelt;
        hasSheild = Player.hasSheild;

    }
}
