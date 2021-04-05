using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingMarker : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer[] Markers;

    //[HideInInspector] 
    public bool ShowMarkers = false;

    private void Start()
    {
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

            Vector3 Direction = Camera.main.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Direction);

            foreach (SpriteRenderer SR in Markers)
            {
                SR.enabled = true;
            }
        }
        else
        {
            animator.enabled = false;

            foreach (SpriteRenderer SR in Markers)
            {
                SR.enabled = false;
            }
        }
    }
}
