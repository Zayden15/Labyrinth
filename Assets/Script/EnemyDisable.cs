using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisable : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            enemy.SetActive(false);
        }

    }

}
