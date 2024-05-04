using System.Collections;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private int rotationAngle = 90;
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
            StartCoroutine(RotateCell());
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

    IEnumerator RotateCell()
    {
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
