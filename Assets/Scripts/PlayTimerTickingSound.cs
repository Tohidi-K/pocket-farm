using UnityEngine;
using System.Collections;

public class PlayTimerTickingSound : MonoBehaviour
{
    public AudioSource audioSource;
    private TextDisplayManager textDisplayManager;

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
            textDisplayManager.countdownText.color = Color.red;
            audioSource.Play();
            yield return new WaitUntil(() => textDisplayManager.currentTime > 9);
            textDisplayManager.countdownText.color = Color.white;
            audioSource.Stop();
        }
    }
}
