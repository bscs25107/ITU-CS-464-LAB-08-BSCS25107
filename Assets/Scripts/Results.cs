using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour
{
    public CurrencyLabel currency;
    public TMP_Text routeText, scoreText;
    public RectTransform[] bars;        // 3 bade bars
    public Image[] barImages;           // wohi 3 bars

    static readonly Color cyan = new Color(0.176f, 0.886f, 0.902f);
    static readonly Color dim = new Color(0.725f, 0.639f, 0.851f, 0.35f);

    void OnEnable() { StartCoroutine(Play()); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ScreenManager.I.Rewind("MainMenu");
    }

    IEnumerator Play()
    {
        int score = GameSession.score;
        int earned = score >= 50 ? 3 : score >= 20 ? 2 : 1;

        routeText.text = "ROUTE " + GameSession.level.ToString("00") + " COMPLETE";
        scoreText.text = "000";
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].localScale = Vector3.zero;
            barImages[i].color = i < earned ? cyan : dim;
        }

        yield return new WaitForSecondsRealtime(0.5f);      // screen ke slide hone ka intezar

        yield return Tween.Run(1.2f, Ease.OutExpo, t =>
            scoreText.text = Mathf.RoundToInt(score * t).ToString("000"));

        for (int i = 0; i < bars.Length; i++)
        {
            StartCoroutine(Pop(bars[i]));
            yield return new WaitForSecondsRealtime(0.15f);
        }
        GameSession.scrap += 25;
        currency.CountTo(GameSession.scrap);
    }

    IEnumerator Pop(RectTransform r)
    {
        yield return Tween.Run(0.3f, Ease.OutBack,
            t => r.localScale = Vector3.LerpUnclamped(Vector3.zero, Vector3.one, t));
    }
}