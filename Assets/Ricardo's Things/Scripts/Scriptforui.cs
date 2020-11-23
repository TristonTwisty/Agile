using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Scriptforui : MonoBehaviour
{
    // Start is called before the first frame update
    public Text displayPlayerHealth;
    public float playerHealth;
    public int totalDashAmount;
    public int currentDashAmount;
    public Text displayTotalDashAmount;
    public UnityEngine.UI.Slider playerHealthSlider;
    public UnityEngine.UI.Image dashRecharge;
    public UnityEngine.UI.Slider shieldRechargeSlider;
    public float shieldRecharge;
  

    void Start()
    {
        currentDashAmount = 0;
        totalDashAmount = 2;
        playerHealth = 100;

        shieldRecharge = 100;
        shieldRechargeSlider.value = shieldRecharge;
        playerHealthSlider.value = playerHealth;
        displayPlayerHealth.text = playerHealth.ToString();
        displayTotalDashAmount.text = currentDashAmount.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        shieldRechargeSlider.value = shieldRecharge;
        playerHealth = playerHealthSlider.value;
        displayPlayerHealth.text = playerHealth.ToString();
        
        if(currentDashAmount < totalDashAmount)
        {
            dashRecharge.fillAmount = (dashRecharge.fillAmount + .001f);
            
            if(dashRecharge.fillAmount == 1)
            {
                currentDashAmount = currentDashAmount + 1;
                displayTotalDashAmount.text = currentDashAmount.ToString();
                dashRecharge.fillAmount = 0;
            }
        }

 

    }






}
