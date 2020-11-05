using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RailGrinding : MonoBehaviour
{
    [SerializeField] private GameObject Hoverboard = null;
    [SerializeField] private float Speed = 5f;
    [Tooltip("Rail particle system to let player know they can grind")][SerializeField] private ParticleSystem RailParticle = null;
    private bool CanGrind = false;

    [Header("Tweener")]
    [Tooltip("How the board follows the rail path")][SerializeField] private PathType Rail = PathType.CatmullRom;
    [Tooltip("Put in the rail waypoints to create rail path")] [SerializeField] private Vector3[] PathValue = null;

    private void Update()
    {
        if (CanGrind && Input.GetKeyDown("e"))
        {
            Hoverboard.GetComponent<HoverboardInput>().enabled = false;
            Hoverboard.transform.DOPath(PathValue, Speed, Rail);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hoverboard"))
        {
            RailParticle.Play();
            CanGrind = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hoverboard"))
        {
            RailParticle.Stop();
            CanGrind = false;
        }
    }
}
