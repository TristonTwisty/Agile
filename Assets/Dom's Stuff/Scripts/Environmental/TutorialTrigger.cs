using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Text;

    private void Start()
    {
        Text.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Text.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Text.SetActive(false);
    }
}
