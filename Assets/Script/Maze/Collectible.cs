using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] int pointsForOnePickup = 1;
    bool wasCollected = false;
    public static int numCollected = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            numCollected++;
            FindObjectOfType<GameSession>().AddAsCollected(pointsForOnePickup);
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    

}
