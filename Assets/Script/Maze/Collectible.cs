using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private static int collectibleCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectibleCount++;
            Debug.Log("Collectible gathered! Total: " + collectibleCount);

            Destroy(gameObject);
        }
    }

    public static bool CheckCount()
    {
        if (collectibleCount == 3) { return true; }
        return false;
    }
}
