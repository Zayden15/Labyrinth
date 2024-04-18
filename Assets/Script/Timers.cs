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
    private float[] lightsMin = new float[] { 0, 4, 5, 6 };
    private float[] lightsMax = new float[] { 0, 7, 9, 11 };

    //Increases by a max of 30 and a min of 10 every second, variation of 30
    private int[] ambienceAverage = new int[] { 0, 150, 180, 210 };

    //Link to audio script
    [SerializeField]
    private AudioManagerScript audioManager;

    //Get player and enemy
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject enemyObject;

    

    // Start is called before the first frame update
    void Start()
    {
 
        //Ambience setup
        ambienceRandom = Random.Range(ambienceAverage[getLevel()] - 30, ambienceAverage[getLevel()] + 30);
        Debug.Log("Random ambience timer is " + ambienceRandom);
        StartCoroutine(ambienceManager());

        //Lights setup
        lightsRandom = Random.Range(lightsMin[getLevel()], lightsMax[getLevel()]);
        Debug.Log("Random lights timer is "+ lightsRandom);

        //Audio setup
        audioManager = FindObjectOfType<AudioManagerScript>();

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
        //float flickerTimer = 0.0f;
        isFlickering = true;
        Debug.Log("Flickering lights");
        audioManager.PlayAudio("lightsFlickerOn");
        yield return new WaitForSeconds(2.5f);

        //Set player view cone to ignore walls?


        //Make light fade in
        /*while (flickerTimer < 0.5f)
        {
            yield return new WaitForSeconds(0.1f);
            flickerTimer += Time.deltaTime;
            //Do a thing that slightly increases lights
        }*/


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
                ambienceRandom = Random.Range(ambienceAverage[getLevel()] - 30, ambienceAverage[getLevel()] + 30);
                switch (getEnemyDistance())
                {
                    case 1:
                        audioManager.PlayAudio("ambienceFar");
                        break;
                    case 2:
                        audioManager.PlayAudio("ambienceMedium");
                        break;
                    case 3:
                        audioManager.PlayAudio("ambienceClose");
                        break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    int getEnemyDistance()
    {
        float playerEnemyDistance = Vector3.Distance(playerObject.transform.position, enemyObject.transform.position);
        if (playerEnemyDistance <= 16f)
        {
            return 3;
        }
        else if (playerEnemyDistance <= 32f)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    int getLevel()
    {
        //Return level of game
        return 1;
    }

}
