using ECM.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public GameObject FPSCam;
    public GameObject FPSCanvas;
    public GameObject ThirdPersonCam;
    public GameObject Player;
    public GameObject ParticleDisc;
    private MouseLook ML;
    private NewRingToss RT;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        ML = Player.GetComponent<MouseLook>();
        RT = ParticleDisc.GetComponent<NewRingToss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (FPSCam.activeInHierarchy == true)
            {
                RT.ThirdPerson = true;
                Player.transform.rotation = Quaternion.Euler(0, 180, 0);
                FPSCanvas.SetActive(false);
                FPSCam.SetActive(false);
                ThirdPersonCam.SetActive(true);
                ML.lateralSensitivity = 0;
                ML.verticalSensitivity = 0;
            }
            else if (ThirdPersonCam.activeInHierarchy == true)
            {
                RT.ThirdPerson = false;
                FPSCanvas.SetActive(true);
                FPSCam.SetActive(true);
                ThirdPersonCam.SetActive(false);
                ML.lateralSensitivity = 2;
                ML.verticalSensitivity = 2;
            }
        }
    }
}