using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySeekerNav : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private SphereCollider enemyCollider;

    //Timers tranfer
    private int ambienceCounter = 0;
    private int ambienceRandom;

    //Increases by a max of 40 and a min of 20 every second, value is +- 30
    private int[] ambienceAverage = new int[] { 0, 300, 350, 400, 450, 500 };

    //Link to audio script
    [SerializeField]
    private AudioManagerScript audioManager;


    void Start()
    {
        //Ambience setup
        ambienceRandom = Random.Range(ambienceAverage[getLevel()] / 2, ambienceAverage[getLevel()]);
        StartCoroutine(ambienceManager());
        //Audio setup
        audioManager = FindObjectOfType<AudioManagerScript>();

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

    IEnumerator ambienceManager()
    {
        for (; ; )
        {
            ambienceCounter += getPlayerDistance();
            if (ambienceCounter >= ambienceRandom)
            {
                ambienceCounter = 0;
                ambienceRandom = Random.Range(ambienceAverage[getLevel()] - 40, ambienceAverage[getLevel()] + 40);
                switch (getPlayerDistance())
                {
                    case 2:
                        audioManager.PlayAudio("ambienceFarSeeker");
                        break;
                    case 3:
                        audioManager.PlayAudio("ambienceMediumSeeker");
                        break;
                    case 4:
                        audioManager.PlayAudio("ambienceCloseSeeker");
                        break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    int getPlayerDistance()
    {
        //change player to enemy
        int distanceValue = 2;
        float playerEnemyDistance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if (playerEnemyDistance <= 16f)
        {
            distanceValue = 4;
        }
        else if (playerEnemyDistance <= 32f)
        {
            distanceValue = 3;
        }
        else
        {
            distanceValue = 2;
        }
        //Check LOS, add 1 if in LOS (and clamp to max of 4)
        return distanceValue;
    }

    int getLevel()
    {
        //Return level of game
        return 1;
    }
}
