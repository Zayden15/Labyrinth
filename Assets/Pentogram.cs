using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentogram : MonoBehaviour
{
    [SerializeField] private GameObject summon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Collectible.numCollected != 0)
        {
            Debug.Log("Summoning");
            Collectible.numCollected--;
            summon.SetActive(true);
        }
    }
}
