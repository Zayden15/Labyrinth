using System.Collections;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private bool playerInside = false;
    private bool isRotating = false;

    private void Start()
    {
        ActivateRotationPlatform();
    }

    void Update()
    {
        if (playerInside && !isRotating && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(RotateCellLeft());
        }
        else if (playerInside && !isRotating && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(RotateCellRight());
        }
    }

    private void ActivateRotationPlatform() {
        GameObject ChildGameObject1 = this.transform.GetChild(0).gameObject;
        ChildGameObject1.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    IEnumerator RotateCellLeft()
    {
        int rotationAngle = 90;
        isRotating = true;

        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = transform.localRotation * Quaternion.Euler(0, rotationAngle, 0);
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rotationSpeed / 90.0f;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        isRotating = false;
    }

    IEnumerator RotateCellRight()
    {
        int rotationAngle = -90;
        isRotating = true;

        Quaternion startRotation = transform.localRotation;
        Quaternion targetRotation = transform.localRotation * Quaternion.Euler(0, rotationAngle, 0);
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rotationSpeed / 90.0f;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        isRotating = false;
    }

}
