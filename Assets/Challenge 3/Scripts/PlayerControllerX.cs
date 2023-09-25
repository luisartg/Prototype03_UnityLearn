using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    public float topLimit;
    public float topPadding;
    public float bottomLimit;
    private float gravityModifier = 1.5f;
    
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip boingSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        MakeJump(5);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            MakeJump(floatForce);
        }

        CheckConstraints();
    }

    private void CheckConstraints()
    {
        CheckUpperLimit();
        CheckDownLimit();
    }

    private void CheckUpperLimit()
    {
        if (PlayerAboveLimit())
        {
            VoidVelocityAndSetToYPosition(topLimit);
            MakeJump(-5);
        }
    }

    private void CheckDownLimit()
    {
        if (transform.position.y < bottomLimit && !gameOver)
        {
            VoidVelocityAndSetToYPosition(bottomLimit);
            MakeJump(10);
            playerAudio.PlayOneShot(boingSound, 1);
        }
    }

    private void MakeJump(float force)
    {
        if (!PlayerAboveLimit(topPadding))
        {
            playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

    private bool PlayerAboveLimit()
    {
        return PlayerAboveLimit(0);
    }

    private bool PlayerAboveLimit(float padding)
    {
        return (transform.position.y > (topLimit - padding));
    }

    private void VoidVelocityAndSetToYPosition(float yPos)
    {
        playerRb.velocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
