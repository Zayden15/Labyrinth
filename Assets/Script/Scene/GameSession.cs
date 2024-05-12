using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 100;
    [SerializeField] int collectiblesNeededToCollect = 5;

    [SerializeField] int collectiblesCollected = 0;
  
    [SerializeField] TextMeshProUGUI collectiblesNeeded;
    [SerializeField] TextMeshProUGUI collectiblesCount; 

    private int currentSceneIndex;

    public int CollectiblesNeededToCollect
    {
        get { return collectiblesNeededToCollect; }
        set { collectiblesNeededToCollect = value; }
    }

    public int CollectiblesCollected
    {
        get { return collectiblesCollected; }
        set { collectiblesCollected = value;}
    }

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();

    }

    public void UpdateUI() {
        collectiblesNeeded.text = collectiblesNeededToCollect.ToString();
        collectiblesCount.text = collectiblesCollected.ToString();
    }
    public void ProcessPlayerDeath() {
        if (playerLives > 1)
        {
            if (TakeLife())
            {
                DontDestroyOnLoad(gameObject);
            }
            UpdateUI();
        }
        else 
        {
            ResetGameSession();
        }
    }

    public void AddAsCollected(int numberToAdd) 
    {
        collectiblesCollected += numberToAdd;

        UpdateUI();
    }

    private bool TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(currentSceneIndex);
        UpdateUI();
        return playerLives > 0;
    }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(currentSceneIndex);
        Destroy(gameObject);
    }

}
