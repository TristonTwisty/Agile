using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShift : MonoBehaviour
{
    private Transform Player;

    [SerializeField] private float RaiseTime = 2;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player = other.transform;
                StartCoroutine(ShiftPlayer());
            }
        }
    }

    private IEnumerator ShiftPlayer()
    {
        float t = 0;

        RigidbodyFPS GravityControl = Player.GetComponent<RigidbodyFPS>();

        GravityControl.CanWalk = false;
        GravityControl.canJump = false;

        float InvertedGravity = GravityControl.gravity;
        GravityControl.gravity = -InvertedGravity;

        Quaternion StartRotation = Player.rotation;
        Quaternion InverseRotation = Quaternion.Euler(StartRotation.eulerAngles);
        Quaternion TargetRotation = new Quaternion(InverseRotation.x, StartRotation.y, InverseRotation.z, StartRotation.w);

        while (t < RaiseTime)
        {
            t += Time.deltaTime;
            Player.rotation = Quaternion.Lerp(StartRotation, TargetRotation, t / RaiseTime);
            yield return null;
        }
    }
}
