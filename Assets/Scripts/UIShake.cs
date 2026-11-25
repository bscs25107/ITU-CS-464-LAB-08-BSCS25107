using System.Collections;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    public float amplitude = 16f;
    RectTransform rt;
    Coroutine run;

    void Awake() { rt = (RectTransform)transform; }

    public void Play()
    {
        if (run != null) return;
        run = StartCoroutine(Routine());
    }

    IEnumerator Routine()
    {
        Vector2 start = rt.anchoredPosition;
        yield return Tween.Run(0.4f, Ease.Linear, t =>
        {
            float amp = amplitude * (1f - Ease.OutCubic(t));
            rt.anchoredPosition = start + new Vector2(Mathf.Sin(t * Mathf.PI * 8f) * amp, 0f);
        });
        rt.anchoredPosition = start;
        run = null;
    }
}