using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentogram : MonoBehaviour
{
    [SerializeField] private GameObject summon;
    private SphereCollider collider;

    private void Start()
    {
        collider = gameObject.GetComponent<SphereCollider>();
        if (collider == null)
        {
            Debug.LogError("SphereCollider not found on the GameObject!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Collectible.numCollected > 0)
        {
            Debug.Log("Summoning");
            Collectible.numCollected--;
            collider.enabled = false;
            summon.SetActive(true);
        }
    }
}

