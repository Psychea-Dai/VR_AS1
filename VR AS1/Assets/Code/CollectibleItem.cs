using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Header("Sound effect")]
    public AudioClip collectSound;
    public float volume = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 播放音效
            if (collectSound != null)
                AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);

            // 原来的拾取逻辑
            GameManager.Instance.CollectItem();
            gameObject.SetActive(false);
        }
    }
}
