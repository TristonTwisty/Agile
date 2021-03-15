using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneLoad : MonoBehaviour
{
    public string LevelName;

    public bool ShouldLoad;
    public bool isLoaded;
    public bool unload;

    private void Update()
    {
        triggerCheck();
    }

    void LoadScene()
    {
        if (!isLoaded)
        {
            SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    void UnLoadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(LevelName);
            isLoaded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
            ShouldLoad = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !unload)
        {
            ShouldLoad = false;
        }
    }

    private void triggerCheck()
    {
        if (ShouldLoad)
        {
            LoadScene();
        }
        else 
        {
            UnLoadScene();
        }
    }

}
