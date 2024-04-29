using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ScLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildNavMesh()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }


}
