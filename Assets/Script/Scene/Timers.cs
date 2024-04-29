using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{

    private float globalTimer = 0.0f;
    private float lightsTimer = 0.0f;
    
    private float lightsRandom;
    private bool isFlickering = false;

    //In seconds, can use decimals
    private float[] lightsMin = new float[] { 0, 5, 6, 7, 10 };
    private float[] lightsMax = new float[] { 0, 10, 12, 14, 18 };


    //Link to audio script
    [SerializeField]
    private AudioManagerScript audioManager;

    //Get player and enemy
    [SerializeField]
    private GameObject playerObject;

    //Get player vision layermask
    private VisionCone playerVisionScript;

    

    // Start is called before the first frame update
    void Start()
    {
 
        //Random values are lower on average at start
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

        //Off
        playerVisionScript.visionObstructingLayer = LayerMask.GetMask("Walls", "Obstructions");
        Debug.Log("Lights have turned off again");
        audioManager.PlayAudio("lightsFlickerOff");
        isFlickering = false;
        yield break;
    }

    

    int getLevel()
    {
        //Return level of game
        return 1;
    }


}
