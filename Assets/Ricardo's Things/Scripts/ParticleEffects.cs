using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public GameObject dashEffect;
    public bool countDown;
    public int countInt;
    // Start is called before the first frame update
    void Start()
    {
        countDown = false;
        countInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dashActive();

        if(countDown == true)
        {
            countInt = countInt + 1;
            if(countInt == 34)
            {
                countInt = 0;
                dashEffect.gameObject.SetActive(false);
                countDown = false;
            }
        }
      
    }


    public void dashActive()
    {
        if (Scriptforui.instance.hasDash == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) == true)
            {
                dashEffect.gameObject.SetActive(true);
                countDown = true;
              

            }
        }
    }

    
 
}






