using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public TextMeshProUGUI scoreText;
    public AudioSource playerAudio;
    public AudioClip jumpSound;
    public float jumpVolume;
    public AudioClip crashSound;
    public float crashVolume;

    public float jumpForce = 10;
    public float gravityModifier = 1;
    public bool isGameOver = false;

    private bool isOnGround = true;
    private bool doubleJumpEnabled = true;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private int score = 0;
    public bool turboEnabled = false;
    private SceneResetControll sceneControl;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = FindObjectOfType<TextMeshProUGUI>();
        sceneControl = GetComponent<SceneResetControll>();
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0, -9.8f, 0) * gravityModifier;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Turbo();
    }

    private void Jump()
    {
        if (isGameOver)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                isOnGround = false;
                JumpAction();
            }
            else if (doubleJumpEnabled)
            {
                doubleJumpEnabled = false;
                JumpAction();
            }
        }
    }

    private void JumpAction()
    {
        playerRb.velocity = Vector3.zero;
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnim.SetTrigger("Jump_trig");
        playerAnim.SetBool("Jump_b", true);
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, jumpVolume);
    }

    private void Turbo()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            turboEnabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            turboEnabled = false;
        }
        UpdatePlayerAnimation();
    }

    private void UpdatePlayerAnimation()
    {
        if (turboEnabled)
        {
            playerAnim.speed = 2;
        }
        else
        {
            playerAnim.speed = 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled) return;

        if (collision.gameObject.CompareTag("Ground") && !isGameOver)
        {
            isOnGround = true;
            doubleJumpEnabled = true;
            playerAnim.SetBool("Jump_b", false);
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !isGameOver)
        {
            isGameOver = true;
            PrintInfo("GAME OVER!");
            PlayDeathAnim();
            playerAudio.PlayOneShot(crashSound, crashVolume);
            Invoke(nameof(ActivateSceneControl), 3f);
        }
    }

    private void PlayDeathAnim()
    {
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
        explosionParticle.Play();
        dirtParticle.Stop();
    }

    private void StartRunningAnimation()
    {
        playerAnim.SetFloat("Speed_f", 1);
        dirtParticle.Play();
    }

    private void PrintInfo(string additionalData = "")
    {
        scoreText.text = $"SCORE: {score}\n{additionalData}";
    }

    private void ActivateSceneControl()
    {
        sceneControl.enabled = true;
        PrintInfo("GAME OVER!\nPress space to restart.");
    }

    public void AddScore(int value, AudioClip soundFx, float soundVolume)
    {
        score += value;
        playerAudio.PlayOneShot(soundFx, soundVolume);
        PrintInfo();
    }

    public void StartGame()
    {
        enabled = true;
        StartRunningAnimation();
    }

    public bool IsGameActive()
    {
        return enabled;
    }
}
