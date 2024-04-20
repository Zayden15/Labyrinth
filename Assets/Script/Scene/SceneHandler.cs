using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] SceneHandler sceneHandler;

    private void Awake()
    {
        /*if (sceneHandler == null)
        {
            sceneHandler = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
    }


    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Tiles");
        Debug.Log("Scene is loaded");
    }
}
