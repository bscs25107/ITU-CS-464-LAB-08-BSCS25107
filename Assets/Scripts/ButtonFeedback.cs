using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    Vector3 baseScale;
    Coroutine c;

    void Awake() { baseScale = transform.localScale; }
    void OnEnable() { transform.localScale = baseScale; }

    void To(Vector3 target, float d, Func<float, float> e)
    {
        if (c != null) StopCoroutine(c);
        c = StartCoroutine(Go(target, d, e));
    }

    IEnumerator Go(Vector3 target, float d, Func<float, float> e)
    {
        Vector3 from = transform.localScale;
        yield return Tween.Run(d, e, t => transform.localScale = Vector3.LerpUnclamped(from, target, t));
    }

    public void OnPointerDown(PointerEventData _) => To(baseScale * 0.92f, 0.08f, Ease.OutCubic);
    public void OnPointerUp(PointerEventData _) => To(baseScale, 0.25f, Ease.OutBack);
    public void OnPointerExit(PointerEventData _) => To(baseScale, 0.15f, Ease.OutCubic);
}