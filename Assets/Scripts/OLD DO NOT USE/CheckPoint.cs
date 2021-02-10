//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CheckPoint : MonoBehaviour
//{

//    public Inventory inventory;
//    public GameObject LocalCheckPoint;
//    public bool destru;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void OnTriggerEnter(Collider other)
//    {
//        GameObject invobj = GameObject.FindGameObjectWithTag("Inventory");
//        inventory = invobj.GetComponent <Inventory>();
//        inventory.CheckPoint = LocalCheckPoint;
//        inventory.SavePlayer();

//        if (destru)
//        {
//            Destroy(gameObject);
//        }

//    }

//}
