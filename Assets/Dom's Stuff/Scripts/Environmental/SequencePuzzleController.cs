using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzleController : MonoBehaviour
{
    [SerializeField] private GameObject[] SequenceObJects;

    [Header("Object to move")]
    private Transform MovePosition;

    [Header("Puzzle Objects")]
    public float ResetTimer = 5;
    public Color ActivationColor;

    private void Start()
    {
        MovePosition = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        foreach (GameObject item in SequenceObJects)
        {
            if(item.GetComponent<SequencePuzzleObject>().Activated == true)
            {
                transform.position = Vector3.Lerp(transform.position, MovePosition.position, 5 * Time.deltaTime);
            }
        }
    }
}
