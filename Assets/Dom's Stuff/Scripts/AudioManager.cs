using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public float FadeSpeed;
    public bool FadeMusic;
    public AudioSource Music;

    private void Start()
    {
        Music.volume = 0;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Update()
    {
        if (!FadeMusic)
        {
            Music.volume += FadeSpeed * Time.deltaTime;
        }

        if(Music.volume >= 1)
        {
            Music.volume = 1;
        }

        if (FadeMusic)
        {
            Music.volume -= FadeSpeed * Time.deltaTime;
        }

        if(Music.volume <= 0f && FadeMusic)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FadeMusic = true;
    }
}
