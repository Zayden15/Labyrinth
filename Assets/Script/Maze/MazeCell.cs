using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] BoxCollider mazeCellCollider;
    [SerializeField] Transform mazeTransform;
    [SerializeField] float rotationSpeed = 100f;
    private int rotationAngle = 90;
    private bool playerInside = false;
    private bool isRotating = false;

    void Start()
    {

    }

    void Update()
    {
        if (playerInside && !isRotating && Input.GetKeyDown(KeyCode.E)) 
        {
            StartCoroutine(RotateCell()); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInside = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInside = false; 
        }
    }

    IEnumerator RotateCell()
    {
        isRotating = true; 

        Quaternion startRotation = mazeTransform.rotation;
        Quaternion targetRotation = mazeTransform.rotation * Quaternion.Euler(0, rotationAngle, 0); 

        float t = 0.0f; 

        while (t < 1.0f)
        {
            t += Time.deltaTime * rotationSpeed / 90.0f; 
            mazeTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, t); 
            yield return null;
        }

        isRotating = false; 
    }
}
