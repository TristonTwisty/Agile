//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GetOnBoard : MonoBehaviour
//{

//    public Inventory script;

//    private void Start()
//    {
//        script = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            Debug.Log("Sending information to inventory");
//            script.OnTriggerEnterBoardFunction();
//        }

//    }
//}
