using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private SphereCollider enemyCollider;

    [SerializeField] private Vector3 tDest;
    private Vector3 ogPos;
    private Vector3 currentTarget;
    private NavMeshAgent agent;
    private float checkRate = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ogPos = transform.position;
        currentTarget = tDest;
        agent.destination = currentTarget; 
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating(nameof(CheckArrival), 0.1f, checkRate);
    }


    private void OnTriggerEnter(Collider enemyCollider)
    {
        if (enemyCollider.CompareTag("Player"))
        {
            Debug.Log("Captured");
            CapturePlayer(enemyCollider.gameObject);
        }
    }

    private void CapturePlayer(GameObject player)
    {
        player.SetActive(false);
        Debug.Log("Game Over");
    }

    void CheckArrival()
    {
        if (Vector3.Distance(transform.position, currentTarget) < 0.5f)
        {
            ToggleDestination();
        }
    }

    void ToggleDestination()
    {
        currentTarget = currentTarget == tDest ? ogPos : tDest;
        agent.destination = currentTarget;
    }
}
