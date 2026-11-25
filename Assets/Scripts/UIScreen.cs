using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIScreen : MonoBehaviour
{
    public bool handlesEscape;   // true = ye screen khud Escape sambhalti hai
    public RectTransform panel;     // the stretched child
    public bool isOverlay;          // true for Pause
    CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        if (panel == null) panel = (RectTransform)transform.GetChild(0);
    }

    // dir = +1 forward (enter from right), -1 back (enter from left)
    public IEnumerator In(float dir)
    {
        gameObject.SetActive(true);
        cg.interactable = false;
        cg.alpha = 0f;
        Vector2 start = new Vector2(dir * 140f, 0f);
        if (isOverlay) start = new Vector2(0f, -80f);
        yield return Tween.Run(isOverlay ? 0.3f : 0.4f, isOverlay ? Ease.OutBack : Ease.OutCubic, t =>
        {
            cg.alpha = Mathf.Clamp01(t);
            panel.anchoredPosition = Vector2.LerpUnclamped(start, Vector2.zero, t);
        });
        cg.interactable = true;
    }

    public IEnumerator Out(float dir)
    {
        cg.interactable = false;
        Vector2 end = isOverlay ? new Vector2(0f, -80f) : new Vector2(-dir * 140f, 0f);
        yield return Tween.Run(0.25f, Ease.InCubic, t =>
        {
            cg.alpha = 1f - t;
            panel.anchoredPosition = Vector2.LerpUnclamped(Vector2.zero, end, t);
        });
        gameObject.SetActive(false);
    }
}