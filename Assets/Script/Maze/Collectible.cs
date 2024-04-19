using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Static counter to track collected items across all instances
    public static int collectibleCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            collectibleCount++; // Increment the collectible count
            Debug.Log("Collectible gathered! Total: " + collectibleCount);

            Destroy(gameObject); // Destroy the collectible
        }
    }
}
