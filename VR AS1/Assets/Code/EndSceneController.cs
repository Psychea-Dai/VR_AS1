using UnityEngine;
using System.Collections;
using TMPro;

public class EndSceneController : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public float waitBeforeQuit = 6f;

    void Start()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        // 文字淡入
        if (endText != null)
        {
            Color c = endText.color;
            c.a = 0f;
            endText.color = c;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 0.5f;
                c.a = t;
                endText.color = c;
                yield return null;
            }
        }

        yield return new WaitForSeconds(waitBeforeQuit);
        Application.Quit();
    }
}
