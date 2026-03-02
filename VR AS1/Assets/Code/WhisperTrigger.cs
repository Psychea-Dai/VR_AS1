using UnityEngine;

public class WhisperTrigger : MonoBehaviour
{
    public AudioClip whisperClip;
    private bool hasPlayed = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.volume = 0.8f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;
        if (!other.CompareTag("Player")) return;
        hasPlayed = true;
        if (whisperClip != null)
            audioSource.PlayOneShot(whisperClip);
    }

    public void Reset()
    {
        hasPlayed = false;
    }
}