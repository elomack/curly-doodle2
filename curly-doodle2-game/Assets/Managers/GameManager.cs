using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winGameUI;
    public GameObject endGameAudioManager;

    public void WinGame()
    {
        GameObject audioManager = GameObject.Find("AudioManager");
        Destroy(audioManager);

        endGameAudioManager.SetActive(true);
        AudioManager endGameAudio = endGameAudioManager.GetComponent<AudioManager>();
        endGameAudio.Play("WinGame");

        Debug.Log("game won");
        winGameUI.SetActive(true);
    }

}
