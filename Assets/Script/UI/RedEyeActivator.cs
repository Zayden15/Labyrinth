using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEyeActivator : MonoBehaviour
{
    [SerializeField] GameObject redEye;

    void Start()
    {
        redEye.SetActive(false);
    }

    public void PointerHover()
    {
        redEye.SetActive(true);

    }

    public void PointerExitHover()
    {
        redEye.SetActive(false);

    }

}
