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
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
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
