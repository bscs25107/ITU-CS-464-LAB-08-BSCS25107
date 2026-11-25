using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleSwitch : MonoBehaviour
{
    public RectTransform knob;
    public Image track;
    public TMP_Text stateLabel;
    public float travel = 76f;               // knob ka kul safar

    public bool IsOn { get; private set; }
    public event Action<bool> Changed;

    static readonly Color onColor = new Color(0.176f, 0.886f, 0.902f);        // #2DE2E6
    static readonly Color offColor = new Color(0.725f, 0.639f, 0.851f, 0.35f);

    Coroutine run;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Set(!IsOn, false, true));
    }

    public void Set(bool value, bool instant, bool notify = false)
    {
        IsOn = value;
        float x = value ? travel / 2f : -travel / 2f;
        Color c = value ? onColor : offColor;
        if (stateLabel != null) stateLabel.text = value ? "ON" : "OFF";

        if (run != null) StopCoroutine(run);
        if (instant)
        {
            knob.anchoredPosition = new Vector2(x, 0f);
            track.color = c;
        }
        else
        {
            run = StartCoroutine(Slide(x, c));
        }
        if (notify) Changed?.Invoke(value);
    }

    IEnumerator Slide(float x, Color c)
    {
        float x0 = knob.anchoredPosition.x;
        Color c0 = track.color;
        yield return Tween.Run(0.2f, Ease.OutCubic, t =>
        {
            knob.anchoredPosition = new Vector2(Mathf.Lerp(x0, x, t), 0f);
            track.color = Color.Lerp(c0, c, t);
        });
    }
}