using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemsInGame ItemToPickup;

    public enum ItemsInGame
    {
        Whip, Disk, Board, Batt, Sheild, Dash
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;

            if(player.GetComponent<Player>() != null)
            {
                switch (ItemToPickup)
                {
                    case ItemsInGame.Whip:
                        player.GetComponent<Player>().Inventory.Add(new WhipItem());
                        player.GetComponent<Player>().haswhip = true;
                        break;
                    case ItemsInGame.Disk:
                        player.GetComponent<Player>().Inventory.Add(new DiskItem());
                        player.GetComponent<Player>().hasdisk = true;
                        break;
                    case ItemsInGame.Board:
                        player.GetComponent<Player>().Inventory.Add(new BoardItem());
                        player.GetComponent<Player>().hasboard = true;
                        break;
                    case ItemsInGame.Batt:
                        player.GetComponent<Player>().Inventory.Add(new BattItem());
                        player.GetComponent<Player>().hasbatt = true;
                        break;
                    case ItemsInGame.Sheild:
                        player.GetComponent<Player>().Inventory.Add(new SheildItem());
                        player.GetComponent<Player>().hassheild = true;
                        break;
                    case ItemsInGame.Dash:
                        player.GetComponent<Player>().Inventory.Add(new DashItem());
                        player.GetComponent<Player>().hasdash = true;
                        break;
                }

                Destroy(gameObject);
            } 
        }
    }
}
