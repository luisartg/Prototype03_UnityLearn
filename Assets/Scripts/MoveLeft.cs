using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 30;
    private float turboMultiplier = 1;
    private PlayerController playerControllerScript;

    private float leftBound = -30;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTurbo();
        Move();
        RemoveIfOutOfBound();
    }

    private void CheckIfTurbo()
    {
        if (playerControllerScript.turboEnabled)
        {
            turboMultiplier = 2;
        }
        else
        {
            turboMultiplier = 1;
        }
    }

    private void Move()
    {
        if (!playerControllerScript.isGameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * turboMultiplier, Space.World);
        }
    }

    private void RemoveIfOutOfBound()
    {
        if (transform.position.x < leftBound && 
            (gameObject.CompareTag("Obstacle") || gameObject.CompareTag("Money")))
        {
            Destroy(gameObject);
        }
    }
}
