using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSounds : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource audioSource;
    public AudioClip grapplingLaunch;
    public AudioClip shieldActivated;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
   

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
