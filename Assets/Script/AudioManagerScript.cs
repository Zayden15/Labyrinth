using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    //Audio initialising, set in inspector
    [SerializeField] AudioSource lightsFlickerOn;
    [SerializeField] AudioSource lightsFlickerOff;
    [SerializeField] AudioSource ambienceFar1;
    [SerializeField] AudioSource ambienceFar2;
    [SerializeField] AudioSource ambienceMedium1;
    [SerializeField] AudioSource ambienceMedium2;
    [SerializeField] AudioSource ambienceMedium3;
    [SerializeField] AudioSource ambienceClose1;
    [SerializeField] AudioSource ambienceClose2;

    int Rand2;
    int Rand3;


    // Start is called before the first frame update
    void Start() { }
    // Update is called once per frame
    void Update(){}


    //Manage general audio, here to have all audio components in one place
    //Random.Range (min, max) MAX IS EXCLUSIVE, min is inclusive
    public void PlayAudio(string AudioFileType)
    {
        Rand2 = Random.Range(1, 3);
        Rand3 = Random.Range(1, 4);

        switch (AudioFileType)
        {
            case "lightsFlickerOn":
                lightsFlickerOn.Play();
                break;

            case "lightsFlickerOff":
                lightsFlickerOff.Play();
                break;

            case "ambienceFar":
                switch (Rand2)
                {
                    case 1:
                        ambienceFar1.Play();
                        break;
                    case 2:
                        ambienceFar2.Play();
                        break;
                }
                break;

            case "ambienceMedium":
                switch (Rand3)
                {
                    case 1:
                        ambienceFar1.Play();
                        break;
                    case 2:
                        ambienceMedium2.Play();
                        break;
                    case 3:
                        ambienceMedium3.Play();
                        break;
                }
                break;

            case "ambienceClose":
                switch (Rand2)
                {
                    case 1:
                        ambienceClose1.Play();
                        Debug.Log("audio1");
                        break;
                    case 2:
                        ambienceClose2.Play();
                        Debug.Log("audio 2");
                        break;
                }
                break;

        }
        //end of audio switch
    }
}
