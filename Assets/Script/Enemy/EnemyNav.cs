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

    private bool isChasing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        agent = GetComponent<NavMeshAgent>();
        ogPos = transform.position;
        currentTarget = tDest;
        agent.destination = currentTarget; 
    }

    // Update is called once per frame
    void Update()
    {
        Patrolling();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Captured");
            //CapturePlayer();
        }
    }

    void Patrolling()
    {
        if (!isChasing && Vector3.Distance(transform.position, currentTarget) < 0.5f)
        {
            ToggleDestination();
        }
    }

    void ToggleDestination()
    {
        currentTarget = currentTarget == tDest ? ogPos : tDest;
        agent.destination = currentTarget;
    }

    public void chasePlayer()
    {
        isChasing = true;
        agent.destination = player.transform.position;
    }

    public void stopChase()
    {
        isChasing = false;
        currentTarget = tDest;
        agent.destination = currentTarget;
    }
}
