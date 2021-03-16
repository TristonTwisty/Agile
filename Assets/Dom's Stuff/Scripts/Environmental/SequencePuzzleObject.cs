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

    private IEnumerator ActivationTimer()
    {
        Activated = true;
        MR.material.color = ActivatedColor;

        yield return new WaitForSeconds(FailTimer);

        Activated = false;
        MR.material.color = OriginalColor;
    }

    public void StartFailTimer()
    {
        Debug.Log("Starting");
        StartCoroutine(ActivationTimer());
    }
}