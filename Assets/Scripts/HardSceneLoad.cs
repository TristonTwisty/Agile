using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HardSceneLoad : MonoBehaviour
{
    public string Scene;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(Scene);
    }
}
