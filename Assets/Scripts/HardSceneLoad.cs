using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardSceneLoad : MonoBehaviour
{
    public string Scene;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }
}
