using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutScreen : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public string title;
        [TextArea] public string description;
        [Range(0f, 1f)] public float range, speed, filter;
        public Button button;
        public Outline outline;
        public TMP_Text selectedTag;
    }

    public Item[] items;
    public TMP_Text itemName, itemDesc;
    public Image rangeFill, speedFill, filterFill;

    static readonly Color cyan = new Color(0.176f, 0.886f, 0.902f);   // #2DE2E6
    static readonly Color pink = new Color(1f, 0.478f, 0.941f);       // #FF7AF0

    Coroutine fills;

    void Awake()
    {
        for (int i = 0; i < items.Length; i++)
        {
            int index = i;                                   // loop variable ki copy
            items[i].button.onClick.AddListener(() => Select(index, false));
        }
    }

    void OnEnable()
    {
        Select(GameSession.loadout, true);                   // pichhla chunav wapas dikhao
    }

    void Select(int i, bool instant)
    {
        GameSession.loadout = i;

        for (int k = 0; k < items.Length; k++)
        {
            bool on = k == i;
            Color c = on ? pink : new Color(cyan.r, cyan.g, cyan.b, 0.25f);
            items[k].outline.effectColor = c;
            items[k].outline.effectDistance = on ? new Vector2(4f, 4f) : new Vector2(3f, 3f);
            items[k].selectedTag.text = on ? "SELECTED" : "";
        }

        itemName.text = items[i].title;
        itemDesc.text = items[i].description;

        if (fills != null) StopCoroutine(fills);
        if (instant)
        {
            rangeFill.fillAmount = items[i].range;
            speedFill.fillAmount = items[i].speed;
            filterFill.fillAmount = items[i].filter;
        }
        else
        {
            fills = StartCoroutine(AnimateFills(items[i]));
        }
    }

    IEnumerator AnimateFills(Item it)
    {
        float r0 = rangeFill.fillAmount, s0 = speedFill.fillAmount, f0 = filterFill.fillAmount;
        yield return Tween.Run(0.35f, Ease.OutCubic, t =>
        {
            rangeFill.fillAmount = Mathf.Lerp(r0, it.range, t);
            speedFill.fillAmount = Mathf.Lerp(s0, it.speed, t);
            filterFill.fillAmount = Mathf.Lerp(f0, it.filter, t);
        });
    }
}