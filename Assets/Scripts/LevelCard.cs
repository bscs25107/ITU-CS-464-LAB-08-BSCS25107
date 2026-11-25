using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelCard : MonoBehaviour
{
    public enum State { Cleared, Current, Locked }

    public int level = 1;
    public State state = State.Locked;
    [Range(0, 3)] public int bars = 0;          // kamayi hui signal strength

    public TMP_Text tag;
    public Image[] barImages;                   // 3 chhoti bars
    public GameObject lockOverlay;
    public Outline outline;

    static readonly Color cyan = new Color(0.176f, 0.886f, 0.902f);      // #2DE2E6
    static readonly Color pink = new Color(1f, 0.478f, 0.941f);          // #FF7AF0
    static readonly Color muted = new Color(0.725f, 0.639f, 0.851f);     // #B9A3D9

    RectTransform rt;
    Coroutine shake;

    void Awake()
    {
        rt = (RectTransform)transform;
        GetComponent<Button>().onClick.AddListener(OnClick);
        Apply();
    }

    void Apply()
    {
        lockOverlay.SetActive(state == State.Locked);

        tag.text = state == State.Cleared ? "CLEARED" : state == State.Current ? "NEW" : "";
        tag.color = state == State.Current ? pink : muted;

        for (int i = 0; i < barImages.Length; i++)
        {
            Color c = i < bars ? cyan : muted;
            if (i >= bars) c.a = 0.35f;
            barImages[i].color = c;
        }

        Color o = state == State.Current ? pink : cyan;
        if (state != State.Current) o.a = 0.25f;
        outline.effectColor = o;
        outline.effectDistance = state == State.Current ? new Vector2(4f, 4f) : new Vector2(3f, 3f);
    }

    void Update()
    {
        if (state != State.Current) return;
        float s = 0.5f + 0.5f * Mathf.Sin(Time.unscaledTime * 3f);   // dheema pulse, flashing nahi
        Color c = pink;
        c.a = 0.55f + 0.45f * s;
        outline.effectColor = c;
    }

    void OnClick()
    {
        if (state == State.Locked) { Shake(); return; }
        GameSession.level = level;
        ScreenManager.I.Open("Loadout");
    }

    void Shake()
    {
        if (shake != null) return;
        shake = StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        Vector2 start = rt.anchoredPosition;
        yield return Tween.Run(0.4f, Ease.Linear, t =>
        {
            float amp = 16f * (1f - Ease.OutCubic(t));
            rt.anchoredPosition = start + new Vector2(Mathf.Sin(t * Mathf.PI * 8f) * amp, 0f);
        });
        rt.anchoredPosition = start;
        shake = null;
    }
};