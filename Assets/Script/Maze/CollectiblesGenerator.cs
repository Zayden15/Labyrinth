using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour
{
    [SerializeField] GameObject collectiblePrefab;
    [SerializeField] int numOfCollectibles = 3;
    private SphereCollider collectibleCollider;

    
    void Start()
    {
        collectibleCollider = collectiblePrefab.GetComponent<SphereCollider>();
    }


    void Update()
    {
       // GenerateCollectibles();
    }

    private void GenerateCollectibles()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider collectibleCollider)
    {
        collectiblePrefab.SetActive(false);
    }
}
