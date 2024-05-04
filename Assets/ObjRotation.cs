using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    public GameObject objectToRotate;
    Quaternion targetRotation;

    private void Update()
    {
        CheckRotation();
    }

    void CheckRotation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRotation = Quaternion.Euler(objectToRotate.transform.eulerAngles.x, objectToRotate.transform.eulerAngles.y - 90, objectToRotate.transform.eulerAngles.z);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetRotation = Quaternion.Euler(objectToRotate.transform.eulerAngles.x, objectToRotate.transform.eulerAngles.y + 90, objectToRotate.transform.eulerAngles.z);
        }

        objectToRotate.transform.rotation = targetRotation;
    }
}
