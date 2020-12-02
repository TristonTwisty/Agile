using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public float[] position;

    public bool hasboard;
    public bool hasdisk;
    public bool haswhip;

    public string BoardParent;
    public string DiskParent;
    public string WhipParent;

    public PlayerData (Inventory Player)
    {
        health = Player.Health;

        position = new float[3];
        position[0] = PlayerRefs.instance.Player.transform.position.x;
        position[1] = PlayerRefs.instance.Player.transform.position.y;
        position[2] = PlayerRefs.instance.Player.transform.position.z;

        hasboard = Player.hasboard;
        hasdisk = Player.hasdisk;
        haswhip = Player.haswhip;

        

    }
}
