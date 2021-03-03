using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    //[SerializeField] private List<GameObject> Enemies;
    [SerializeField] private GameObject[] Enemies;

    [SerializeField] private Transform MovePosition;

    private void Update()
    {
        if (Enemies.Length == 0)
        {
            transform.position = Vector3.Lerp(transform.position, MovePosition.position, 5);
        }
    }
}
