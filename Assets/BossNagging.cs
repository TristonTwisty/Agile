using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNagging : MonoBehaviour
{
    public AudioClip[] BossLines;

    public void PlayIntLine(int i)
    {
        AudioManager.instance.PlaySfx(BossLines[i],.5f);
    }
}
