using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private SphereCollider enemyCollider;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.localPosition;
        enemyCollider = player.GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider enemyCollider)
    {
        if (enemyCollider.CompareTag("Player"))
        {
            CapturePlayer(enemyCollider.gameObject);
        }
    }

    private void CapturePlayer(GameObject player)
    {
        player.SetActive(false);
        Debug.Log("Game Over");
    }
}
