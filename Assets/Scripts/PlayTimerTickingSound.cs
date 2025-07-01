using System.Collections;
using UnityEngine;

public class PlayTimerTickingSound : MonoBehaviour
{
    private TextDisplayManager textDisplayManager;
    public AudioSource audioSource;

    private void Awake()
    {
        textDisplayManager = GameObject.Find("UIManager").GetComponent<TextDisplayManager>();
    }

    private void Start()
    {
        StartCoroutine(TimerSound());
    }

    private IEnumerator TimerSound()
    {
        while (textDisplayManager.currentTime >= 0)
        {
            yield return new WaitUntil(() => textDisplayManager.isCurrentTimeInitialized && textDisplayManager.currentTime < 9);
            audioSource.Play();
            yield return new WaitUntil(() => textDisplayManager.currentTime > 9);
            audioSource.Stop();
        }
    }
}
