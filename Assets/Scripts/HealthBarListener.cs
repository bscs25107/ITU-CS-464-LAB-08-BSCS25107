// 1. UI: HealthBarListener.cs
using UnityEngine;
using UnityEngine.UI;

public class HealthBarListener : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] Image fill;           // Image Type = Filled

    void OnEnable() { health.Damaged += Refresh; health.Healed += Refresh; }
    void OnDisable() { health.Damaged -= Refresh; health.Healed -= Refresh; }
    void Start() => Refresh(health.Current, health.Max);   // Start, not OnEnable
    void Refresh(int c, int m) => fill.fillAmount = (float)c / m;
}