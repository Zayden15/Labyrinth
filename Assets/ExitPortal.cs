using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] int levelLoadDelay = 1;
    [SerializeField] GameSession gameSession;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && gameSession.CollectiblesCollected == gameSession.CollectiblesNeededToCollect)
        {
            Debug.Log(gameSession.CollectiblesCollected);
            //Debug.Log("Portal entered");
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }

        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);

    }
}
