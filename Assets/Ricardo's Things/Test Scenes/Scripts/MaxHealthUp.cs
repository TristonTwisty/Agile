using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider Player)
    {
        Debug.Log("player entered");
        PlayerRefs.instance.PlayerHealth += 10;
        PlayerRefs.instance.currentHealth = PlayerRefs.instance.PlayerHealth;
        Scriptforui.instance.playerHealthSlider.maxValue = PlayerRefs.instance.currentHealth;
        Scriptforui.instance.playerHealthSlider.value = Scriptforui.instance.playerHealthSlider.maxValue;
        Destroy(gameObject);

    }
}
