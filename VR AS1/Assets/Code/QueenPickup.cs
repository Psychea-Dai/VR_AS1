using UnityEngine;
using System.Collections;

public class QueenPickup : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private bool grabbed = false;
    private Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // 检测物体是否突然移动（被抓起来时会跟着手移动）
        float moved = Vector3.Distance(transform.position, lastPosition);
        if (!grabbed && moved > 0.05f)
        {
            grabbed = true;
            if (pickupSound != null && audioSource != null)
                audioSource.PlayOneShot(pickupSound);
            StartCoroutine(ResetGrab());
        }
        lastPosition = transform.position;
    }

    IEnumerator ResetGrab()
    {
        yield return new WaitForSeconds(2f);
        grabbed = false;
    }
}