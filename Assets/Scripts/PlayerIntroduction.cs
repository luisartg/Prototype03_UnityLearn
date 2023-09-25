using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntroduction : MonoBehaviour
{
    public float startingX;
    public float walkUntilX;
    public float walkSpeed;
    private PlayerController playerController;
    private Animator animator;
    private MoveLeft bgMoveLeft;
    private SpawnManager spawnManager;
    private AudioSource cameraAudioSrc;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        FindComponents();
        playerRb.isKinematic = true;
        transform.position = new Vector3(startingX, 0, 0);
        playerRb.isKinematic = false;
        animator.SetFloat("Speed_f", 0.26f); //this is minimum walking speed
    }

    private void FindComponents()
    {
        Debug.Log("Finding Components for starting introduction");
        playerRb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        GameObject bgGO = GameObject.Find("Background");
        bgMoveLeft = (bgGO != null) ? bgGO.GetComponent<MoveLeft>() : null;
        spawnManager = FindObjectOfType<SpawnManager>();
        GameObject cameraGO = GameObject.Find("Main Camera");
        cameraAudioSrc = (cameraGO != null) ? cameraGO.GetComponent<AudioSource>() : null;
    }

    // Update is called once per frame
    void Update()
    {
        MoveRight();
        CheckIfPlayerOnPosition();
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * walkSpeed * Time.deltaTime, Space.World);
    }

    private void CheckIfPlayerOnPosition() 
    {
        if (transform.position.x >= walkUntilX)
        {
            transform.position = new Vector3(walkUntilX, transform.position.y, transform.position.z);
            playerController.StartGame();
            if (bgMoveLeft!=null) bgMoveLeft.enabled = true;
            if (spawnManager != null) spawnManager.enabled = true;
            if (cameraAudioSrc!= null) cameraAudioSrc.enabled = true;
            Debug.Log("GAME START");
            enabled = false;
        }
    }
}
