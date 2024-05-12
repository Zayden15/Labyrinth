using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWake : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private void Start()
    {
        //enemy.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            enemy.SetActive(true);
        }

        else if (other.CompareTag("Enemy")) {
            Debug.Log("Enemy entered ");
            enemy.SetActive(false);
        }
    }
}
