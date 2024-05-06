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
    public static void LoadLevel1()
    {
        SceneManager.LoadScene("Maze");

    }
    public static void LoadLevel2()
    {
        SceneManager.LoadScene("2ndLevel");

    }
    public static void LoadLevel3()
    {
        SceneManager.LoadScene("Maze");

    }
    public static void LoadLevel4()
    {
        SceneManager.LoadSceneAsync("Level4");

    }

}
