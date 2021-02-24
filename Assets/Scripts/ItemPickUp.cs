using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemsInGame ItemToPickup;

    //Added For UI
    public Scriptforui scriptForUI;
    private void Start()
    {
        scriptForUI = GameObject.FindObjectOfType<Scriptforui>();
    }
    //End Added For UI
    


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
                        //Added For UI
                        scriptForUI.firstItem.gameObject.SetActive(true);
                        //End Added For UI
                        break;
                    case ItemsInGame.Sheild:
                        player.GetComponent<Player>().Inventory.Add(new SheildItem());
                        player.GetComponent<Player>().hassheild = true;
                        //Added For UI
                        scriptForUI.shieldImage.gameObject.SetActive(true); 
                        scriptForUI.shieldRechargeSlider.gameObject.SetActive(true);
                        //End Added For UI
                        break;
                    case ItemsInGame.Dash:
                        player.GetComponent<Player>().Inventory.Add(new DashItem());
                        player.GetComponent<Player>().hasdash = true;
                        //Added For UI
                        scriptForUI.playerDash.gameObject.SetActive(true);
                        scriptForUI.displayTotalDashAmount.gameObject.SetActive(true);
                        scriptForUI.hasDash = true;
                        //End Added For UI
                        break;
                }

                Destroy(gameObject);
            } 
        }
    }
}
