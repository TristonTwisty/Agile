using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzleObject : MonoBehaviour
{
    [HideInInspector] public bool Activated = false;
    private MeshRenderer MR;
    private Color OriginalColor;

    [Header("Sequence Controller")]
    [SerializeField] private SequencePuzzleController PuzzleController;
    private float FailTimer;
    private Color ActivatedColor;

    private void Start()
    {
        gameObject.tag = "Target";

        MR = GetComponent<MeshRenderer>();
        OriginalColor = MR.material.color;

        ActivatedColor = PuzzleController.ActivationColor;
        FailTimer = PuzzleController.ResetTimer;

        transform.parent = null;
    }

    private void Update()
    {
        if (Activated)
        {
            MR.material.color = ActivatedColor;
        }
    }

    private IEnumerator ActivationTimer()
    {
        Activated = true;

        yield return new WaitForSeconds(FailTimer);

        Activated = false;
        MR.material.color = OriginalColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Particle Disc"))
        {
            StartCoroutine(ActivationTimer());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Particle Disc"))
        {
            StartCoroutine(ActivationTimer());
        }
    }
}