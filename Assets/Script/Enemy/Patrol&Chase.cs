using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private List<Vector3> Destination;
    private int currentDestinationIndex = 0;
    private Vector3 ogPos;
    private Vector3 currentTarget;
    private NavMeshAgent agent;

    private bool isChasing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        if (Destination.Count > 0)
        {
            currentTarget = Destination[0];
            agent.destination = currentTarget;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Patrolling();
    }
 

    void Patrolling()
    {
        if (!isChasing && Vector3.Distance(transform.position, currentTarget) < 0.2f)
        {
            ToggleDestination();
        }
    }

    void ToggleDestination()
    {
        if (Destination.Count == 0) return;

        currentDestinationIndex = (currentDestinationIndex + 1) % Destination.Count;
        currentTarget = Destination[currentDestinationIndex];
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
        currentTarget = Destination[currentDestinationIndex];
        agent.destination = currentTarget;
    }
}
