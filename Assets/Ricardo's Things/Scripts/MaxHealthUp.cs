using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUp : MonoBehaviour
{
    [SerializeField] private float RotationSpeed = 50;

    void Update()
    {
        transform.Rotate(transform.up * (RotationSpeed * Time.deltaTime));
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
