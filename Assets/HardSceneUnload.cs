using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardSceneUnload : MonoBehaviour
{
    public string Scene;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.UnloadSceneAsync(Scene);
        gameObject.SetActive(false);
    }
}
