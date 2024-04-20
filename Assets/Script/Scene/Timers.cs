using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{

    private float globalTimer = 0.0f;
    private float lightsTimer = 0.0f;
    private int ambienceCounter = 0;
    private float lightsRandom;
    private int ambienceRandom;
    private bool isFlickering = false;

    //In seconds, can use decimals
    private float[] lightsMin = new float[] { 0, 5, 6, 7 };
    private float[] lightsMax = new float[] { 0, 10, 12, 14 };

    //Increases by a max of 40 and a min of 20 every second, value is +- 30
    private int[] ambienceAverage = new int[] { 0, 250, 300, 350 };

    //Link to audio script
    [SerializeField]
    private AudioManagerScript audioManager;

    //Get player and enemy
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject enemyObject;

    //Get player vision layermask
    private VisionCone playerVisionScript;

    

    // Start is called before the first frame update
    void Start()
    {
 
        //Random values are lower on average at start
        //Ambience setup
        ambienceRandom = Random.Range(ambienceAverage[getLevel()]/2, ambienceAverage[getLevel()]);
        Debug.Log("Random ambience timer is " + ambienceRandom);
        StartCoroutine(ambienceManager());

        //Lights setup
        lightsRandom = lightsMin[getLevel()];
        Debug.Log("Random lights timer is "+ lightsRandom);

        //Audio setup
        audioManager = FindObjectOfType<AudioManagerScript>();

        //get playerVision Script
        playerVisionScript = playerObject.GetComponentInChildren< VisionCone >();

    }

    // Update is called once per frame
    void Update()
    {
        globalTimer += Time.deltaTime;
        if (!isFlickering)
        {
            lightsTimer += Time.deltaTime;
        }

        if (lightsTimer > lightsRandom)
        {
            lightsTimer = 0;
            lightsRandom = Random.Range(lightsMin[getLevel()], lightsMax[getLevel()]);
            StartCoroutine(flickerLights());
        }


    }

    IEnumerator flickerLights()
    {
        //On
        isFlickering = true;
        Debug.Log("Flickering lights");
        audioManager.PlayAudio("lightsFlickerOn");
        playerVisionScript.visionObstructingLayer = LayerMask.GetMask("Obstructions");
        yield return new WaitForSeconds(2.1f);
        if (playerObject)
        {
            Debug.Log("AAAAA");
        }
        else
        {
            Debug.Log("where?");
        }
        //Off
        playerVisionScript.visionObstructingLayer = LayerMask.GetMask("Walls", "Obstructions");
        Debug.Log("Lights have turned off again");
        audioManager.PlayAudio("lightsFlickerOff");
        isFlickering = false;
        yield break;
    }

    IEnumerator ambienceManager()
    {
        for (; ; )
        {
            ambienceCounter += getEnemyDistance();
            if (ambienceCounter >= ambienceRandom)
            {
                ambienceCounter = 0;
                ambienceRandom = Random.Range(ambienceAverage[getLevel()] - 40, ambienceAverage[getLevel()] + 40);
                switch (getEnemyDistance())
                {
                    case 2:
                        audioManager.PlayAudio("ambienceFar");
                        break;
                    case 3:
                        audioManager.PlayAudio("ambienceMedium");
                        break;
                    case 4:
                        audioManager.PlayAudio("ambienceClose");
                        break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    int getEnemyDistance()
    {
        int distanceValue = 2;
        float playerEnemyDistance = Vector3.Distance(playerObject.transform.position, enemyObject.transform.position);
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
