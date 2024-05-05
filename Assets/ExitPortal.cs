using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (Collectible.CheckCount())
            {
                Debug.Log("true");
                SceneHandler.LoadLevel4();
            }
        }
    }
}
