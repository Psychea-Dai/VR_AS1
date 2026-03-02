using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Rendering;

public class EndSceneController : MonoBehaviour
{
    public TextMeshProUGUI endText;
     public float fadeInDuration = 2f;
    public float holdDuration = 4f;
    public float waitBeforeQuit = 6f;

    void Start()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        // 文字从透明淡入
        endText.alpha = 0f;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeInDuration;
            endText.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        yield return new WaitForSeconds(holdDuration);

        // 文字淡出
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeInDuration;
            endText.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }


        yield return new WaitForSeconds(waitBeforeQuit);
        Application.Quit();
    }
}
