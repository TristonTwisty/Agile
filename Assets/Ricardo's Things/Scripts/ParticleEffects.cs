using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        dashActive();

 
      
    }


    public void dashActive()
    {
        if (Scriptforui.instance.hasDash == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) == true)
            {


            }
        }
    }

    
 
}






