using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    public static PlayerPrefs instance;

    private float _MusicVolume;
    private float _sfxVolume;

    private void Awake()
    {
        if (PlayerPrefs.instance == null)
        {
            PlayerPrefs.instance = this;
        }
        else if (PlayerPrefs.instance != this)
        {
            Destroy(this);
        }
    }
}
