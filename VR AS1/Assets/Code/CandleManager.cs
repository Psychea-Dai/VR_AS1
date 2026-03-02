using UnityEngine;
using System.Collections;

public class CandleManager : MonoBehaviour
{
    public static CandleManager Instance;

    [Header("三个蜡烛的Light子物体，按顺序拖入")]
    public Light candle1Light;
    public Light candle2Light;
    public Light candle3Light;

    [Header("点亮音效")]
    public AudioClip flameSound;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void LightCandle(int itemCount)
    {
        // itemCount是当前收集数量，1/2/3对应点亮哪根蜡烛
        Light target = null;

        if (itemCount == 1) target = candle1Light;
        else if (itemCount == 2) target = candle2Light;
        else if (itemCount == 3) target = candle3Light;

        if (target != null)
            StartCoroutine(FadeInLight(target));
    }

    IEnumerator FadeInLight(Light light)
    {
        // 播放音效
        if (flameSound != null)
            AudioSource.PlayClipAtPoint(flameSound, light.transform.position);

        // 灯光从0渐变亮起，有"被点燃"的感觉
        light.gameObject.SetActive(true);
        light.intensity = 0f;
        float targetIntensity = 1.5f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 0.8f;
            light.intensity = Mathf.Lerp(0f, targetIntensity, t);
            yield return null;
        }
    }
}