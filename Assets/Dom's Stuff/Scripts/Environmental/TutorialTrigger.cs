using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject TutorialPanel;

    private void Start()
    {
        TutorialPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        TutorialPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        TutorialPanel.SetActive(false);
    }
}
