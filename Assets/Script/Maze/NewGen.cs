using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewGen : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private int collectibleCount;
    [SerializeField] private GameObject collectable;
    
    [SerializeField] private int gridX;
    [SerializeField] private int gridZ;
    [SerializeField] private float spacing;

    void Start()
    {
        GenerateGrid();
        BuildNavMesh();
        GenerateCollectibles();
    }

    void GenerateGrid()
    {
        //Debug.Log($"Generating Grid with dimensions {gridX}x{gridZ} and spacing {spacing}");
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 position = new Vector3(x * spacing, 0, z * spacing);

                
                int randomRotation = Random.Range(0, 4) * 90;
                if (randomRotation == 270)
                { // Convert 270 to -90 for consistency with your request
                    randomRotation = -90;
                }

                Quaternion rotation = Quaternion.Euler(0, randomRotation, 0);

                GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
                GameObject tile = Instantiate(prefab, position, Quaternion.identity);

                MazeCell mazeCell = tile.AddComponent<MazeCell>();
            }
        }
    }

    void GenerateCollectibles()
    {
        HashSet<Vector3> usedPositions = new HashSet<Vector3>();

        for (int i = 0; i < collectibleCount; i++)
        {
            int x = Random.Range(0, gridX);
            int z = Random.Range(0, gridZ);
            Vector3 position = new Vector3(x * spacing, 0.5f, z * spacing); // Slightly raise the Y to avoid z-fighting

            // Ensure that no two collectibles are placed on the same spot
            if (!usedPositions.Contains(position))
            {
                Debug.Log("Collectible made");
                Instantiate(collectable, position, Quaternion.identity);
                usedPositions.Add(position);
            }
            else
            {
                i--; // Decrement the counter and try again
            }
        }
    }

    void BuildNavMesh()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
