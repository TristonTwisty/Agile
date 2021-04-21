using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePuzzleController : MonoBehaviour
{
    [SerializeField] private GameObject[] SequenceObjects;

    [Header("Object To Move")]
    [SerializeField] private Transform MovePosition;

    [Header("Puzzle Objects")]
    public float ResetTimer = 5;
    public Color ActivationColor;
    public float doorDuration = 5;

    private bool _hasTriggered = false;

    private void Awake()
    {
        MovePosition.parent = null;
    }

    private void Update()
    {
        if (_hasTriggered == false)
        {
            bool isEverythingActive = true;

            foreach (GameObject item in SequenceObjects)
            {
                if (item.GetComponent<SequencePuzzleObject>().Activated == false)
                {
                    isEverythingActive = false;
                }
            }

            if (_hasTriggered == false && isEverythingActive == true)
            {
                StartCoroutine(MoveDoor());
            }
        }
    }

    private IEnumerator MoveDoor()
    {
        _hasTriggered = true;
        float t = 0;

        Debug.Log("Door Open");

        Vector3 startPosition = transform.position;

        while(t < doorDuration)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, MovePosition.position, t/doorDuration);
            yield return null;
        }
    }
}
