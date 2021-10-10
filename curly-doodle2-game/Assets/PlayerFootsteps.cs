using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    public AudioClip walkingClip;
    public AudioClip runningClip;

    void Start()
    {
        playerMovement = PlayerManager.instance.player.GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.Log("test update");
        if (playerMovement.isGrounded &&
            audioSource.isPlaying == false &&
            (playerMovement.GetMoveDirection().x != 0 || playerMovement.GetMoveDirection().z != 0))
        {
            Debug.Log("test if statement");
            audioSource.volume = Random.Range(0.8f, 1f);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            if (playerMovement.isRunning)
                audioSource.clip = runningClip;
            else
                audioSource.clip = walkingClip;
            audioSource.Play();
            Debug.Log(audioSource.ToString());
        }
    }
}
