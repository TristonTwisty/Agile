using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveMesh : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
    }
}
