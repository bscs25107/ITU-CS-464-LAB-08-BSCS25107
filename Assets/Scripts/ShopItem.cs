using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIShake))]
public class ShopItem : MonoBehaviour
{
    public int index;                  // 0, 1, 2
    public int price = 40;
    public Button buyButton;
    public Image buyImage;
    public TMP_Text buyLabel;
    public CurrencyLabel currency;

    static readonly Color cyan = new Color(0.176f, 0.886f, 0.902f);
    static readonly Color ink = new Color(0.039f, 0.020f, 0.063f);       // #0A0510
    static readonly Color muted = new Color(0.725f, 0.639f, 0.851f);     // #B9A3D9
    static readonly Color dimBg = new Color(0.725f, 0.639f, 0.851f, 0.25f);
    static readonly Color pink = new Color(1f, 0.478f, 0.941f);

    RectTransform rt;

    void Awake()
    {
        rt = (RectTransform)transform;
        buyButton.onClick.AddListener(OnBuy);
    }

    void OnEnable() { Refresh(); }

    public void Refresh()
    {
        if (GameSession.owned[index])
        {
            buyLabel.text = "OWNED";
            buyLabel.color = pink;
            buyImage.color = dimBg;
        }
        else if (GameSession.scrap >= price)
        {
            buyLabel.text = "BUY " + price;
            buyLabel.color = ink;
            buyImage.color = cyan;
        }
        else
        {
            buyLabel.text = "NEED " + (price - GameSession.scrap);
            buyLabel.color = muted;
            buyImage.color = dimBg;
        }
    }

    void OnBuy()
    {
        if (GameSession.owned[index]) return;
        if (GameSession.scrap < price) { GetComponent<UIShake>().Play(); return; }

        GameSession.scrap -= price;
        GameSession.owned[index] = true;
        currency.CountTo(GameSession.scrap);
        StartCoroutine(Stamp());

        foreach (var item in transform.parent.GetComponentsInChildren<ShopItem>())
            item.Refresh();
    }

    IEnumerator Stamp()
    {
        yield return Tween.Run(0.35f, Ease.OutBack,
            t => rt.localScale = Vector3.one * Mathf.LerpUnclamped(1.08f, 1f, t));
    }
}