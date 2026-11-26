using UnityEngine;
using UnityEngine.UI;

public class HealthBarListener : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Image fill;                 // Image Type = Filled
    [SerializeField] Color fullColor = Color.cyan;
    [SerializeField] Color lowColor = new Color(1f, 0.3f, 0.6f);

    void OnEnable() { health.Damaged += Refresh; health.Healed += Refresh; }
    void OnDisable() { health.Damaged -= Refresh; health.Healed -= Refresh; }
    void Start() => Refresh(health.Current, health.Max);   // Start, not OnEnable

    void Refresh(int c, int m)
    {
        float t = (float)c / m;
        fill.fillAmount = t;
        fill.color = Color.Lerp(lowColor, fullColor, t);
    }
}