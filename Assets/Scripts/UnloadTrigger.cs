using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadTrigger : MonoBehaviour
{
    public string LevelToUnload;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.UnloadSceneAsync(LevelToUnload);
        Destroy(this);
    }
}
