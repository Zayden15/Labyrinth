using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOutObject : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] LayerMask wallMask;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

   
    void Update()
    {
        Vector2 cutOutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutOutPos.y /= (Screen.width / Screen.height);
        
        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
        
        for (int i = 0; i < hitObjects.Length; i++)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int j = 0; j < materials.Length; j++)
            {
                materials[j].SetVector("_CutOutPosition", cutOutPos);
                materials[j].SetFloat("_CutOutSize", 0.1f );
                materials[j].SetFloat("_FalloffSize", 0.05f);
            }
        }
    }
}
