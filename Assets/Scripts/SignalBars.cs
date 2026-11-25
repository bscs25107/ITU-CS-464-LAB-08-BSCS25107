using UnityEngine;
using UnityEngine.UI;

public class SignalBars : MonoBehaviour
{
    public Image[] bars;
    public Color on = new Color(1f, 0.69f, 0f, 1f);     // #FFB000
    public Color off = new Color(1f, 0.69f, 0f, 0.15f);
    public float cycle = 2.8f;

    void Update()
    {
        float p = Mathf.Repeat(Time.unscaledTime / cycle, 1f);
        float lit = Ease.OutCubic(Mathf.Clamp01(p / 0.6f)) * bars.Length;
        float fade = 1f - Mathf.Clamp01((p - 0.85f) / 0.15f);
        for (int i = 0; i < bars.Length; i++)
        {
            Color c = Color.Lerp(off, on, Mathf.Clamp01(lit - i));
            c.a *= Mathf.Lerp(0.15f, 1f, fade);
            bars[i].color = c;
        }
    }
}