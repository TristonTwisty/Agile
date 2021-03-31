using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public string Level;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync(Level, LoadSceneMode.Additive);
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
