using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] private Material visionConeMaterial;
    [SerializeField] private float visionRange;
    [SerializeField] private float visionAngle;
    [SerializeField] private LayerMask visionObstructingLayer;
    [SerializeField] private int visionConeResolution = 120;
    private Mesh visionConeMesh;
    private MeshFilter meshFilter_;
    
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = visionConeMaterial;
        meshFilter_ = transform.AddComponent<MeshFilter>();
        visionConeMesh = new Mesh();
        visionAngle *= Mathf.Deg2Rad;
    }


    void Update()
    {
        DrawVisionCone();
    }

    void DrawVisionCone()//this method creates the vision cone mesh
    {
        int[] triangles = new int[(visionConeResolution - 1) * 3];
        Vector3[] vertices = new Vector3[visionConeResolution + 1];
        vertices[0] = Vector3.zero;
        float currentAngle = -visionAngle / 2;
        float angleIcrement = visionAngle / (visionConeResolution - 1);
        float sin;
        float cos;

        for (int i = 0; i < visionConeResolution; i++)
        {
            sin = Mathf.Sin(currentAngle);
            cos = Mathf.Cos(currentAngle);
            Vector3 RaycastDirection = (transform.forward * cos) + (transform.right * sin);
            Vector3 VertForward = (Vector3.forward * cos) + (Vector3.right * sin);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, visionRange, visionObstructingLayer))
            {
                vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                vertices[i + 1] = VertForward * visionRange;
            }


            currentAngle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        visionConeMesh.Clear();
        visionConeMesh.vertices = vertices;
        visionConeMesh.triangles = triangles;
        meshFilter_.mesh = visionConeMesh;
    }


}