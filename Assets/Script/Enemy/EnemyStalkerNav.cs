using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStalkerNav : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private SphereCollider enemyCollider;

    //Timers tranfer
    private int ambienceCounter = 0;
    private int ambienceRandom;
    private bool isClose = false;

    //Increases by a max of 40 and a min of 20 every second, value is +- 30
    private int[] ambienceAverage = new int[] { 0, 0, 0, 0, 300, 0 };

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
        //how to change movement speed?
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
                ambienceRandom = Random.Range(ambienceAverage[getLevel()] - 50, ambienceAverage[getLevel()] + 50);
                switch (getPlayerDistance())
                {
                    case 1:
                        audioManager.PlayAudio("ambienceCloseStalker");
                        break;
                    case 10:
                        audioManager.PlayAudio("sprintingStalker");
                        break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private int getPlayerDistance()
    {
        //change player to enemy
        int distanceValue = 2;
        float playerEnemyDistance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if (playerEnemyDistance <= 20.0f)
        {
            if (!isClose)
            {
                //change movement speed permanently
                isClose = true;
            }
            distanceValue = 1;
        }
        else
        {
            distanceValue = 10;
        }
        return distanceValue;
    }

    private int getLevel()
    {
        //Return level of game
        return 1;
    }
}
