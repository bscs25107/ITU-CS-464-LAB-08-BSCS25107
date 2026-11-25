using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CRTOverlay : MonoBehaviour
{
    bool effectsOn = true;
    public RawImage scanlines, vignette, noise;
    public RectTransform shakeTarget;            // SafeArea drag karo
    [Range(0f, 1f)] public float scanlineAlpha = 0.14f;
    [Range(0f, 1f)] public float staticBase = 0.05f;
    public float maxShakePixels = 14f;

    float staticNow;
    float nextNoise;
    Coroutine burst;

    void Awake()
    {
        scanlines.texture = MakeScanlines();
        scanlines.uvRect = new Rect(0f, 0f, 1f, 480f);   // 1920 / 4 repeats
        scanlines.color = new Color(0f, 0f, 0f, scanlineAlpha);

        vignette.texture = MakeVignette();
        noise.texture = MakeNoise(128);

        scanlines.raycastTarget = false;
        vignette.raycastTarget = false;
        noise.raycastTarget = false;

        staticNow = staticBase;
        Apply();
    }

    void Update()
    {
        if (Time.unscaledTime >= nextNoise)
        {
            noise.uvRect = new Rect(Random.value, Random.value, 8f, 14f);
            nextNoise = Time.unscaledTime + 0.08f;
        }
    }

    void Apply() { noise.color = new Color(1f, 1f, 1f, staticNow); }

    // Screen change pe call hota hai
    public void Burst(float peak = 0.28f, float dur = 0.35f)
    {
        if (!effectsOn) return;
        if (burst != null) StopCoroutine(burst);
        burst = StartCoroutine(BurstRoutine(Mathf.Min(peak, 0.3f), dur));
    }
    public void SetEffects(bool on)
    {
        effectsOn = on;
        scanlines.gameObject.SetActive(on);
        vignette.gameObject.SetActive(on);
        noise.gameObject.SetActive(on);
        if (!on && shakeTarget != null) shakeTarget.anchoredPosition = Vector2.zero;
    }
    IEnumerator BurstRoutine(float peak, float dur)
    {
        yield return Tween.Run(dur, Ease.OutCubic, t =>
        {
            float k = 1f - t;                       // 1 -> 0 jaldi decay
            staticNow = Mathf.Lerp(staticBase, peak, k);
            float s = maxShakePixels * k;
            if (shakeTarget != null)
                shakeTarget.anchoredPosition = new Vector2(Random.Range(-s, s), 0f);
            Apply();
        });
        if (shakeTarget != null) shakeTarget.anchoredPosition = Vector2.zero;
        staticNow = staticBase;
        Apply();
    }

    static Texture2D MakeScanlines()
    {
        var tex = new Texture2D(1, 4, TextureFormat.RGBA32, false)
        { wrapMode = TextureWrapMode.Repeat, filterMode = FilterMode.Point };
        tex.SetPixels(new[] {
            new Color(0, 0, 0, 1), new Color(0, 0, 0, 1),
            new Color(0, 0, 0, 0), new Color(0, 0, 0, 0) });
        tex.Apply();
        return tex;
    }

    static Texture2D MakeVignette()
    {
        const int n = 256;
        var tex = new Texture2D(n, n, TextureFormat.RGBA32, false)
        { wrapMode = TextureWrapMode.Clamp, filterMode = FilterMode.Bilinear };
        var px = new Color[n * n];
        for (int y = 0; y < n; y++)
            for (int x = 0; x < n; x++)
            {
                float dx = (x / (n - 1f) - 0.5f) * 2f;
                float dy = (y / (n - 1f) - 0.5f) * 2f;
                float d = Mathf.Sqrt(dx * dx + dy * dy);
                float a = Mathf.SmoothStep(0f, 1f, (d - 0.55f) / 0.8f) * 0.7f;
                px[y * n + x] = new Color(0, 0, 0, a);
            }
        tex.SetPixels(px);
        tex.Apply();
        return tex;
    }

    static Texture2D MakeNoise(int n)
    {
        var tex = new Texture2D(n, n, TextureFormat.RGBA32, false)
        { wrapMode = TextureWrapMode.Repeat, filterMode = FilterMode.Point };
        var px = new Color[n * n];
        for (int i = 0; i < px.Length; i++)
        {
            float g = Random.value;
            px[i] = new Color(g, g, g, 1f);
        }
        tex.SetPixels(px);
        tex.Apply();
        return tex;
    }
}