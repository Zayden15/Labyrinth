using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] SceneHandler sceneHandler;

    private void Awake()
    {

    }


    void Update()
    {
        
    }

    public void LoadTutorialLevel()
    {
        SceneManager.LoadScene("Tiles");
        
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Maze");

    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Maze");

    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Maze");

    }
    public void LoadLevel4()
    {
        SceneManager.LoadScene("Maze");

    }

}
