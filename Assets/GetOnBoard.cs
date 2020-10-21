using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoard : MonoBehaviour
{
    public GameObject BoardCamera;
    public GameObject Board;
    private Component BoardController;
    public GameObject Player;
    public GameObject Boardrider;


    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BoardController = gameObject.GetComponent<HoverboardInput>();
            Player = GameObject.FindGameObjectWithTag("Player");
            BoardCamera.gameObject.SetActive(true);
            GetComponent<HoverboardInput>().enabled = true;
            Boardrider.gameObject.SetActive(true);
            Player.gameObject.SetActive(false);
            Player.transform.parent = Board.transform;
        }

    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            BoardCamera.gameObject.SetActive(false);
            Board.gameObject.SetActive(false);
            GetComponent<HoverboardInput>().enabled = false;
            Boardrider.gameObject.SetActive(false);
            Player.gameObject.SetActive(true);
            Player.transform.parent = null;
        }*/
    }
}
