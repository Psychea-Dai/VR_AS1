using UnityEngine;

public class PlacementZone : MonoBehaviour
{
    public string acceptTag = "Collectible";   // 只接受带这个Tag的物体
    public GameObject visualMarker;            // 那个金色圆盘
    public GameObject nextUnlockObject;        // 放对之后激活什么（比如一个新线索）
      public AudioClip placeSound;
    private bool isSolved = false;

    void OnTriggerEnter(Collider other)
    {
        if (isSolved) return;
        
        if (other.CompareTag(acceptTag))
        {
            isSolved = true;
            
            // 吸附到正确位置
            other.transform.position = transform.position;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
            
            // 播放放置音效
            if (placeSound != null)
                AudioSource.PlayClipAtPoint(placeSound, transform.position);
            
            // 圆盘变绿
            if (visualMarker != null)
                visualMarker.GetComponent<Renderer>().material.color = Color.green;
            
            if (nextUnlockObject != null)
                nextUnlockObject.SetActive(true);
            
            GameManager.Instance.CollectItem();
        }
    }
}