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
        //scriptForUI = GameObject.FindObjectOfType<Scriptforui>();

    }
    //End Added For UI
    


    public enum ItemsInGame
    {
        Whip, Disk, Board, Batt, Sheild, Dash
    }

    private void OnCollisionEnter(Collision collision)
    {
        scriptForUI = GameObject.FindObjectOfType<Scriptforui>();
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;

            if(player.GetComponent<Player>() != null)
            {
                switch (ItemToPickup)
                {
                    case ItemsInGame.Whip:
                        player.GetComponent<Player>().Inventory.Add(new WhipItem());
                        player.GetComponent<Player>().haswhip = true;

                        //Added for UI
                        scriptForUI.dockedItem2.gameObject.SetActive(true);
                        scriptForUI.item2Text.gameObject.SetActive(true);
                        break;
                    case ItemsInGame.Disk:
                        player.GetComponent<Player>().Inventory.Add(new DiskItem());
                        player.GetComponent<Player>().hasdisk = true;

                        //Added For UI
                        scriptForUI.dockedItem1.gameObject.SetActive(true);
                        scriptForUI.item1Text.gameObject.SetActive(true);
                        break;
                    case ItemsInGame.Board:
                        player.GetComponent<Player>().Inventory.Add(new BoardItem());
                        player.GetComponent<Player>().hasboard = true;
                        break;
                    case ItemsInGame.Batt:
                        player.GetComponent<Player>().Inventory.Add(new BattItem());
                        player.GetComponent<Player>().hasbatt = true;
                        //Added For UI
                        player.GetComponent<Player>().Inventory[0].ActivateObject(VisualManager.instace.BattVisual);
                        scriptForUI.firstItem.gameObject.SetActive(true);
                        scriptForUI.currentItemText.gameObject.SetActive(true);
                        //End Added For UI
                        break;
                    case ItemsInGame.Sheild:
                        player.GetComponent<Player>().Inventory.Add(new SheildItem());
                        player.GetComponent<Player>().hassheild = true;
                        //Added For UI
                        scriptForUI.shieldImage.gameObject.SetActive(true); 
                        scriptForUI.shieldRechargeSlider.gameObject.SetActive(true);
                        scriptForUI.hasShield = true;
                        //End Added For UI
                        VisualManager.instace.SheildVisual.SetActive(true);
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
