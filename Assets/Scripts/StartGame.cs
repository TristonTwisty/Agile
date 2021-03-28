using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    PlayerRefs refs;
    [SerializeField] private int timer;
    // Start is called before the first frame update
    void Start()
    {
        refs.Player.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(Timer());
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        refs.Player.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject);
    }
}
