using UnityEngine;
using System.Collections;

public class QueenPickup : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource audioSource;
    private bool grabbed = false;

    private Rigidbody rb;
    private Collider cld;

    private Vector3 oriLocalScale;
    //private Vector3 lastPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //lastPosition = transform.position;
        oriLocalScale = transform.lossyScale;
    }

    void Update()
    {
    }

    public void OnDragQueen(Transform chessHolder)
    {
        if(grabbed) return;

        grabbed = true;
        rb.useGravity = false;
        cld.enabled = false;
        
        transform.SetParent(chessHolder);
        transform.localPosition = Vector3.zero;
        transform.localScale = oriLocalScale;

        if (pickupSound != null && audioSource != null)
            audioSource.PlayOneShot(pickupSound);
    }

    public void OnCancleDrag()
    {
        grabbed = false;
        transform.SetParent(null);
        transform.localScale = oriLocalScale;
        rb.useGravity = true;
        cld.enabled = true;
    }
}