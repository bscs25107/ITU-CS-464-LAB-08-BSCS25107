using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ModeCard : MonoBehaviour
{
    public string modeName;       // Story / Endless / Daily
    public string targetScreen;   // LevelSelect / Loadout
    public bool locked;

    RectTransform rt;
    Coroutine shake;

    void Awake()
    {
        rt = (RectTransform)transform;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Debug.Log("ModeCard click: " + modeName + " -> " + targetScreen + " locked=" + locked);
        if (locked) { Shake(); return; }
        GameSession.mode = modeName;
        ScreenManager.I.Open(targetScreen);
    }

    void Shake()
    {
        if (shake != null) return;           // double-tap pe dobara shuru nahi
        shake = StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        Vector2 start = rt.anchoredPosition;
        yield return Tween.Run(0.4f, Ease.Linear, t =>
        {
            float amp = 16f * (1f - Ease.OutCubic(t));      // jaldi decay
            rt.anchoredPosition = start + new Vector2(Mathf.Sin(t * Mathf.PI * 8f) * amp, 0f);
        });
        rt.anchoredPosition = start;
        shake = null;
    }
}