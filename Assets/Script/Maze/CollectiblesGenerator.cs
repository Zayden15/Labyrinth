using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour
{
    [SerializeField] GameObject collectiblePrefab;
    [SerializeField] int numOfCollectibles = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateCollectibles();
    }

    private void GenerateCollectibles()
    {
        throw new NotImplementedException();
    }
}
