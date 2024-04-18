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
    private float[] lightsMin = new float[] { 3, 4, 5 };
    private float[] lightsMax = new float[] { 4, 5, 6 };

    [SerializeField]  AudioSource lightsFlickerOn;
    [SerializeField]  AudioSource lightsFlickerOff;
    [SerializeField]  AudioSource monsterFar;
    [SerializeField]  AudioSource monsterMedium;
    [SerializeField]  AudioSource monsterClose;

    // Start is called before the first frame update
    void Start()
    {

        //Ambience setup
        StartCoroutine(ambienceManager());

        //Lights setup
        lightsRandom = Random.Range(lightsMin[getLevel()], lightsMax[getLevel()]);
        Debug.Log(lightsRandom);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        globalTimer += Time.deltaTime;
        if (!isFlickering)
        {
            lightsTimer += Time.deltaTime;
        }
        //Make ambient timer increase through a subroutine called once every 10th of a second to increase performance

        Debug.Log("a");

        if (lightsTimer > lightsRandom)
        {
            
            lightsTimer = 0;
            lightsRandom = Random.Range(lightsMin[getLevel()], lightsMax[getLevel()]);
            StartCoroutine(flickerLights());
        }

    }

    IEnumerator flickerLights()
    {
        float flickerTimer = 0.0f;
        isFlickering = true;
        Debug.Log("Flickering lights");
        lightsFlickerOn.Play();
        yield return new WaitForSeconds(2.0f);


        //Make light fade in
        /*while (flickerTimer < 0.5f)
        {
            yield return new WaitForSeconds(0.1f);
            flickerTimer += Time.deltaTime;
            //Do a thing that slightly increases lights
        }*/


        Debug.Log("Lights have stopped");
        lightsFlickerOff.Play();
        isFlickering = false;
        yield break;
    }

    IEnumerator ambienceManager()
    {
        ambienceCounter += getEnemyDistance();
        
        yield return new WaitForSeconds(0.1f);
    }

    int getEnemyDistance()
    {
        //X = getdistance.unity
        //get distance, closer is more
        return 1;

    }

    int getLevel()
    {
        //Return level number
        return 1;
    }

}
