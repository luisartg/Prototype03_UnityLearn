using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip coinSound;
    public float coinVolume;
    public AudioClip comboSound;
    public float comboVolume;
    public int collectibleValue = 0;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.AddScore(collectibleValue, coinSound, coinVolume);
            if (playerController.turboEnabled)
            {
                playerController.AddScore(collectibleValue, comboSound, comboVolume);
            }
            Destroy(gameObject);
        }
        
    }
}
