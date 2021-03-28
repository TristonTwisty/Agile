using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;

    public AudioClip Music1;
    public AudioClip Music2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.instace.PlaySfx(clip1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.instace.PlaySfx(clip2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.instace.PlaySfx(clip2, transform, 1); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioManager.instace.playBGM(Music1, 3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioManager.instace.playBGM(Music2, 3);
        }
    }
}
