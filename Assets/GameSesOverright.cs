using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSesOverright : MonoBehaviour
{
    [SerializeField] int newCollectiblesNeededToCollect;
    [SerializeField] int newCollectiblesCollected;
    [SerializeField] GameSession gameSession;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameSession.CollectiblesNeededToCollect = newCollectiblesNeededToCollect;
            gameSession.CollectiblesCollected = newCollectiblesCollected;
            gameSession.UpdateUI();
        }
    }
}
