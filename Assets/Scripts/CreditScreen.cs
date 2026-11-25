using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour
{
    public RectTransform viewport;
    public RectTransform content;
    public CanvasGroup endCard;
    public float speed = 110f;          // units per second

    Coroutine run;

    void OnEnable()
    {
        if (run != null) StopCoroutine(run);
        run = StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        endCard.alpha = 0f;
        content.anchoredPosition = new Vector2(0f, -viewport.rect.height);

        yield return null;                                   // aik frame, taake layout ban jaye
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        float startY = -viewport.rect.height;                // viewport ke neeche se shuru
        float endY = content.rect.height;                    // poora content upar nikal jaye
        float dur = (endY - startY) / speed;

        yield return new WaitForSecondsRealtime(0.5f);       // screen ke slide hone ka intezar
        yield return Tween.Run(dur, Ease.Linear,
            t => content.anchoredPosition = new Vector2(0f, Mathf.Lerp(startY, endY, t)));

        yield return Tween.Run(0.6f, Ease.OutCubic, t => endCard.alpha = t);
    }
}