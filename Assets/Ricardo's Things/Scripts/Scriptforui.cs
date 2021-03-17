//using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[System.Serializable]
public class Scriptforui : MonoBehaviour
{
    private Player player;
    public Text displayPlayerHealth;
    public float playerHealth;
    public int totalDashAmount;
    public int currentDashAmount;
    public Text displayTotalDashAmount;
    public UnityEngine.UI.Slider playerHealthSlider;
    public UnityEngine.UI.Image dashRecharge;
    public UnityEngine.UI.Slider shieldRechargeSlider;
    public float shieldRecharge;
    public UnityEngine.UI.Image shieldImage;

    //Player Items Holder
    public UnityEngine.UI.Image currentItem;
    public UnityEngine.UI.Image firstItem;
    public UnityEngine.UI.Image secondItem;
    public UnityEngine.UI.Image thirdItem;
    public UnityEngine.UI.Image fourthItem;
    public UnityEngine.UI.Image dockedItem1;
    public UnityEngine.UI.Image dockedItem2;
    public UnityEngine.UI.Image dockedItem3;
    
    //Player Items
    public UnityEngine.UI.Image playerBaton;
    public UnityEngine.UI.Image playerDisc;
    public UnityEngine.UI.Image playerHoverBoard;
    public UnityEngine.UI.Image playerWhip;
    public UnityEngine.UI.Image playerDash;

    public bool itemInSlot1;
    public bool itemInSlot2;
    public bool itemInSlot3;
    public bool itemInSlot4;
    public bool hasDash;
    private ItemPickUp itemPickUp;

    //Sprites
    public Sprite spritePlayerDisc;
    public Sprite spritePlayerWhip;
    public Sprite spritePlayerBoard;
    public Sprite spritePlayerBat;
    public UnityEngine.UI.Text item1Text;
    public UnityEngine.UI.Text item2Text;
    public UnityEngine.UI.Text item3Text;
    public UnityEngine.UI.Text currentItemText;

    void Start()
    {
       
        currentDashAmount = 0;
        totalDashAmount = 3;
        playerHealth = 100;

        shieldRecharge = 100;
        shieldRechargeSlider.value = shieldRecharge;
        playerHealthSlider.value = playerHealth;
        displayPlayerHealth.text = playerHealth.ToString();
        displayTotalDashAmount.text = currentDashAmount.ToString();

        itemInSlot1 = false;
        itemInSlot2 = false;
        itemInSlot3 = false;
        itemInSlot4 = false;

        itemPickUp = ItemPickUp.FindObjectOfType<ItemPickUp>();
     

    }

    // Update is called once per frame
    void Update()
    {
        shieldRechargeSlider.value = shieldRecharge;
        playerHealth = playerHealthSlider.value;
        displayPlayerHealth.text = playerHealth.ToString();
        if (hasDash == true)
        {
            if (currentDashAmount < totalDashAmount)
            {
                dashRecharge.fillAmount = (dashRecharge.fillAmount + .002f);

                if (dashRecharge.fillAmount == 1)
                {
                    currentDashAmount = currentDashAmount + 1;
                    displayTotalDashAmount.text = currentDashAmount.ToString();
                    dashRecharge.fillAmount = 0;
                }
            }
        }

        playerPressedOne();
        pullOutWeapons();
        
        
        
    }

    private void playerPressedOne()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //firstItem.rectTransform.sizeDelta = new Vector2(100, 100);
        }
    }
    private void pullOutWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Swapping Weapons
            firstItem.gameObject.SetActive(true);
            secondItem.gameObject.SetActive(false);
            thirdItem.gameObject.SetActive(false);
            fourthItem.gameObject.SetActive(false);

            //Docking Items
            dockedItem1.sprite = spritePlayerDisc;
            dockedItem2.sprite = spritePlayerBoard;
            dockedItem3.sprite = spritePlayerWhip;
            item1Text.text = "2";
            item2Text.text = "3";
            item3Text.text = "4";
            currentItemText.text = "1";
            dockedItem1.color = Color.blue;//new Color(0, 144, 229, 255);
            dockedItem2.color = Color.magenta;//new Color(161, 0, 231, 255);
            dockedItem3.color = Color.yellow;//new Color(221, 97, 0, 255);



        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Swapping Weapons
            firstItem.gameObject.SetActive(false);
            secondItem.gameObject.SetActive(true);
            thirdItem.gameObject.SetActive(false);
            fourthItem.gameObject.SetActive(false);

            //Docking Items
            dockedItem1.sprite = spritePlayerBat;
            dockedItem2.sprite = spritePlayerBoard;
            dockedItem3.sprite = spritePlayerWhip;
            item1Text.text = "1";
            item2Text.text = "3";
            item3Text.text = "4";
            currentItemText.text = "2";
            dockedItem1.color = Color.red;//new Color(0, 144, 229, 255);
            dockedItem2.color = Color.magenta;//new Color(161, 0, 231, 255);
            dockedItem3.color = Color.yellow;//new Color(221, 97, 0, 255);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Swapping Weapons
            firstItem.gameObject.SetActive(false);
            secondItem.gameObject.SetActive(false);
            thirdItem.gameObject.SetActive(true);
            fourthItem.gameObject.SetActive(false);

            //Docking Items
            dockedItem1.sprite = spritePlayerBat;
            dockedItem2.sprite = spritePlayerDisc;
            dockedItem3.sprite = spritePlayerWhip;
            item1Text.text = "1";
            item2Text.text = "2";
            item3Text.text = "4";
            currentItemText.text = "3";
            dockedItem1.color = Color.red;//new Color(0, 144, 229, 255);
            dockedItem2.color = Color.blue;//new Color(161, 0, 231, 255);
            dockedItem3.color = Color.yellow;//new Color(221, 97, 0, 255);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //Swapping Weapons
            firstItem.gameObject.SetActive(false);
            secondItem.gameObject.SetActive(false);
            thirdItem.gameObject.SetActive(false);
            fourthItem.gameObject.SetActive(true);

            //Docking Items
            dockedItem1.sprite = spritePlayerBat;
            dockedItem2.sprite = spritePlayerDisc;
            dockedItem3.sprite = spritePlayerBoard;
            item1Text.text = "1";
            item2Text.text = "2";
            item3Text.text = "3";
            currentItemText.text = "4";
            dockedItem1.color = Color.red;//new Color(0, 144, 229, 255);
            dockedItem2.color = Color.blue;//new Color(161, 0, 231, 255);
            dockedItem3.color = Color.magenta;//new Color(221, 97, 0, 255);
        }



    }



}
