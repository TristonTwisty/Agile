using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneUnload : MonoBehaviour
{
    public string LevelName;
    public GameObject activate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        activate.SetActive(true);
        SceneManager.UnloadSceneAsync(LevelName);
    }
}
