using System.Collections;
using TMPro;
using UnityEngine;

public class CurrencyLabel : MonoBehaviour
{
    TMP_Text label;
    int shown;
    Coroutine run;

    void Awake() { label = GetComponent<TMP_Text>(); }

    void OnEnable()
    {
        shown = GameSession.scrap;
        Show(shown);
    }

    void Show(int v) { label.text = "SCRAP " + v; }

    public void CountTo(int target)
    {
        if (run != null) StopCoroutine(run);
        run = StartCoroutine(Count(shown, target));
    }

    IEnumerator Count(int from, int to)
    {
        yield return Tween.Run(0.5f, Ease.OutCubic, t =>
        {
            shown = Mathf.RoundToInt(Mathf.Lerp(from, to, t));
            Show(shown);
        });
    }
}