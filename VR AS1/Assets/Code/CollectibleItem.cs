using UnityEngine;
using System.Collections;

public class CollectibleItem : MonoBehaviour
{
    [Header("Sound effect")]
    public AudioClip collectSound;
    public float volume = 1f;

    private bool hasBeenCollected = false;

    public void OnInteract()
    {
        if (hasBeenCollected) return;
        hasBeenCollected = true;

        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);

        GameManager.Instance.CollectItem();
        gameObject.SetActive(false);
    }
}