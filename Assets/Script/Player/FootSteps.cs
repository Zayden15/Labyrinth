using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    private AudioSource audioSource;
    [SerializeField] float minPitch = 0.8f;
    [SerializeField] float maxPitch = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Step() {

        AudioClip clip = GetRandomClip();
        float pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
    }
}
