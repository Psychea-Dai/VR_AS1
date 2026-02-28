using UnityEngine;
using TMPro;
using System.Collections;

public class FloatingHUD : MonoBehaviour
{
    [Header("跟随设置")]
    public Transform scoreTarget;   // 拖入XR Origin，让HUD跟着玩家
    Transform camTrans;

    [Header("UI文字")]
    public TextMeshProUGUI countText;
    public TextMeshProUGUI messageText;

    void Start()
    {
        camTrans = Camera.main.transform;
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (scoreTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, scoreTarget.position, .02f);
            transform.LookAt(new Vector3(camTrans.position.x, transform.position.y, camTrans.position.z));
        }
    }

    public void UpdateCount(int current, int total)
    {
        if (countText != null)
            countText.text = "供物: " + current + "/" + total;
    }

    public void ShowMessage(string msg)
    {
        if (messageText == null) return;
        StopAllCoroutines();
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        StartCoroutine(HideMessage(3f));
    }

    IEnumerator HideMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }
}
