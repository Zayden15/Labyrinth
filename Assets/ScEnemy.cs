using UnityEngine;
using UnityEngine.AI;

public class ScEnemy : MonoBehaviour
{
    [SerializeField] private Vector3 tDest;
    private Vector3 ogPos;
    private Vector3 currentTarget;
    private NavMeshAgent agent;
    private float checkRate = 0.5f; // Check every 0.5 seconds


    private bool isChasing;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        ogPos = transform.position;
        currentTarget = tDest;
        agent.destination = currentTarget; // Set initial destination

        InvokeRepeating(nameof(CheckArrival), 0.1f, checkRate); // Start checking for arrival with some interval
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

    public void chasePlayer()
    {
           agent.destination = player.transform.position;
    }
}
