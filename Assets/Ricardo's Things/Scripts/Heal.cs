using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
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
        if(PlayerRefs.instance.currentHealth < PlayerRefs.instance.PlayerHealth)
        {
            PlayerRefs.instance.currentHealth += 10;
            Scriptforui.instance.playerHealthSlider.value += 10;
            Destroy(gameObject);
        }

    }
}
