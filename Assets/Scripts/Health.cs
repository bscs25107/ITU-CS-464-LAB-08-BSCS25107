using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int max = 100;
    [SerializeField] int current;
    [SerializeField] float invulnSeconds = 0.5f;   // 0 kar do agar nahi chahiye

    public int Current => current;
    public int Max => max;

    public event Action<int, int> Damaged;   // current, max
    public event Action<int, int> Healed;
    public event Action Died;

    float nextHitTime;

    void Awake() => current = max;

    public void Damage(int amount)
    {
        if (current <= 0 || Time.time < nextHitTime) return;
        nextHitTime = Time.time + invulnSeconds;
        current = Mathf.Max(0, current - amount);
        Damaged?.Invoke(current, max);
        if (current == 0) Died?.Invoke();
    }

    public void Heal(int amount)
    {
        if (current <= 0) return;
        current = Mathf.Min(max, current + amount);
        Healed?.Invoke(current, max);
    }
}