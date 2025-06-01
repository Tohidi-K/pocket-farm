using UnityEngine;

public class PlayTimerTickingSound : MonoBehaviour
{
    private TextDisplayManager textDisplayManager;

    public AudioSource audioSource;

    private void Awake()
    {
        textDisplayManager = GameObject.Find("UIManager").GetComponent<TextDisplayManager>();
    }

    private void Update()
    {
        PlayTimerSound();
    }

    private void PlayTimerSound()
    {
        if (textDisplayManager.minutes == 0 && textDisplayManager.seconds < 10)
        {
            audioSource.Play();
        }
    }
}
