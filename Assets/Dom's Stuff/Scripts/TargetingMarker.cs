using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingMarker : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer[] Markers;
    private Transform Cam;

    [HideInInspector] public bool ShowMarkers = false;

    private void Start()
    {
        Cam = Camera.main.transform;

        animator = GetComponent<Animator>();
        animator.enabled = false;

        Markers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer SR in Markers)
        {
            SR.enabled = false;
        }
    }

    private void Update()
    {
        if (ShowMarkers)
        {
            animator.enabled = true;

            foreach (SpriteRenderer SR in Markers)
            {
                SR.enabled = true;
            }
        }
        else
        {
            //animator.enabled = false;

            foreach (SpriteRenderer SR in Markers)
            {
                SR.enabled = false;
            }
        }
    }
}
