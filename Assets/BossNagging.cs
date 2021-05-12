using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNagging : MonoBehaviour
{
    public AudioClip[] BossLines;

    [Header("Fade Controller")]
    [SerializeField] private Animator BlackooutPanel;
    private void Start()
    {
        BlackooutPanel.SetTrigger("Fade In");
    }

    public void PlayIntLine(int i)
    {
        AudioManager.instance.PlaySfx(BossLines[i],.5f);
    }
}
