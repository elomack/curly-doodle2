using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    public AudioClip walkingClip;
    public AudioClip runningClip;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerMovement.isGrounded &&
            audioSource.isPlaying == false &&
            (playerMovement.GetMoveDirection().x != 0 || playerMovement.GetMoveDirection().z != 0))
        {
            audioSource.volume = Random.Range(0.8f, 1f);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            if (playerMovement.isRunning)
                audioSource.clip = runningClip;
            else
                audioSource.clip = walkingClip;
            audioSource.Play();
        }
    }
}