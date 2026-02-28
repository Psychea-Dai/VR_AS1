using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour
{
    public float minOnTime = 2f;
    public float maxOnTime = 8f;
    public float minOffTime = 0.8f;
    public float maxOffTime = 3f;

    private Light pointLight;

    void Start()
    {
        pointLight = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            // 亮着
            pointLight.enabled = true;
            yield return new WaitForSeconds(
                Random.Range(minOnTime, maxOnTime));

            // 快速闪几下
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                pointLight.enabled = false;
                yield return new WaitForSeconds(0.05f);
                pointLight.enabled = true;
                yield return new WaitForSeconds(0.05f);
            }

            // 完全熄灭
            pointLight.enabled = false;
            yield return new WaitForSeconds(
                Random.Range(minOffTime, maxOffTime));
        }
    }
}