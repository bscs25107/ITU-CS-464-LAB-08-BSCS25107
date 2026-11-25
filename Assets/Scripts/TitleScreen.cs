using System.Collections;
using TMPro;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public RectTransform logo;
    public TMP_Text logoText, status, tagline, prompt;
    public Color noSignalColor = new Color(1f, 0.29f, 0.24f);   // #FF4A3D
    public Color foundColor = new Color(0.30f, 0.88f, 1f);      // #4DE1FF

    Vector2 logoEnd;

    void Awake() { logoEnd = logo.anchoredPosition; }
    void OnEnable() { StartCoroutine(Intro()); }

    IEnumerator Intro()
    {
        status.text = "NO SIGNAL";
        status.color = noSignalColor;
        status.alpha = 0f; logoText.alpha = 0f; tagline.alpha = 0f; prompt.alpha = 0f;
        logo.anchoredPosition = logoEnd;

        // 1) NO SIGNAL fade in
        yield return Tween.Run(0.3f, Ease.OutCubic, t => status.alpha = t);
        yield return new WaitForSecondsRealtime(0.25f);

        // 2) signal lock: static burst + logo slides down and fades in
        if (ScreenManager.I != null && ScreenManager.I.crt != null)
            ScreenManager.I.crt.Burst(0.22f, 0.3f);
        Vector2 start = logoEnd + new Vector2(0f, 80f);
        yield return Tween.Run(0.6f, Ease.OutCubic, t =>
        {
            logo.anchoredPosition = Vector2.LerpUnclamped(start, logoEnd, t);
            logoText.alpha = t;
        });

        // 3) status changes to SIGNAL FOUND
        status.text = "SIGNAL FOUND";
        status.color = foundColor;

        // 4) tagline fade in
        yield return Tween.Run(0.4f, Ease.OutCubic, t => tagline.alpha = t);

        // 5) prompt pulse (slow, no flashing)
        while (true)
        {
            float s = 0.5f + 0.5f * Mathf.Sin(Time.unscaledTime * 3f);
            prompt.alpha = 0.45f + 0.55f * s;
            yield return null;
        }
    }
}