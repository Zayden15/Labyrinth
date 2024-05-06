using UnityEngine;
using Cinemachine;
using System.Collections;

public class RotateCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField]
    private Vector3[] rotationCoordinates = { new Vector3(45f, -35f, 0f),
                                              new Vector3(45f, -115f, 0f),
                                              new Vector3(45f, 115f, 0f),
                                              new Vector3(45f, 35f, 0f) };
    private int currentCoordinateIndex = 0;
    private bool isRotating = false;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (!isRotating && Input.GetKeyDown(KeyCode.Space))
        {
            // Get the next rotation coordinate
            Vector3 targetRotation = rotationCoordinates[currentCoordinateIndex];

            // Rotate the camera to the target rotation
            StartCoroutine(RotateCameraTo(targetRotation));

            // Move to the next coordinate index
            currentCoordinateIndex = (currentCoordinateIndex + 1) % rotationCoordinates.Length;
        }
    }

    private IEnumerator RotateCameraTo(Vector3 targetRotation)
    {
        isRotating = true;

        while (Quaternion.Angle(virtualCamera.transform.localRotation, Quaternion.Euler(targetRotation)) > 0.01f)
        {
            // Rotate towards the target rotation
            virtualCamera.transform.localRotation = Quaternion.RotateTowards(virtualCamera.transform.localRotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);

            yield return null;
        }

        isRotating = false;
    }
}
