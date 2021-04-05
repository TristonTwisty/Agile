using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSpin : MonoBehaviour
{
    private void Start()
    {
        this.enabled = false;
    }
    private void Update()
    {
        Vector3 Direction = Camera.main.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Direction);
    }
}
