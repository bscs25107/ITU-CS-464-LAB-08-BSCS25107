using System.Collections;
using UnityEngine;

public class StaggerIn : MonoBehaviour
{
    public RectTransform[] items;
    public float startDelay = 0.15f;
    public float delay = 0.05f;
    public float duration = 0.35f;
    public Vector2 offset = new Vector2(0f, -40f);

    Vector2[] home;
    CanvasGroup[] groups;

    void Awake()
    {
        home = new Vector2[items.Length];
        groups = new CanvasGroup[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            home[i] = items[i].anchoredPosition;
            groups[i] = items[i].GetComponent<CanvasGroup>();
            if (groups[i] == null) groups[i] = items[i].gameObject.AddComponent<CanvasGroup>();
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < items.Length; i++)
        {
            groups[i].alpha = 0f;
            items[i].anchoredPosition = home[i] + offset;
        }
        for (int i = 0; i < items.Length; i++) StartCoroutine(Run(i));
    }

    IEnumerator Run(int i)
    {
        yield return new WaitForSecondsRealtime(startDelay + i * delay);
        Vector2 from = home[i] + offset;
        yield return Tween.Run(duration, Ease.OutBack, t =>
        {
            groups[i].alpha = Mathf.Clamp01(t);
            items[i].anchoredPosition = Vector2.LerpUnclamped(from, home[i], t);
        });
    }
}