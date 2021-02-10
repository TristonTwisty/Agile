//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Pickup : MonoBehaviour
//{
//    public Inventory script;
//    public string FunctionToCall;
//    public string Debug;
//    public GameObject dummy;

//    private void Start()
//    {
//        script = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            print(Debug);
//            script.Invoke(FunctionToCall, 0f);  
//            Destroy(dummy);
//        }

//    }
//}
