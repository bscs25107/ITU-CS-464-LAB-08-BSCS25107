using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] float invulnSeconds = 1f;

    public int Current { get; private set; }
    public int Max => maxHealth;

    public event Action<int, int> Damaged;   // current, max
    public event Action<int, int> Healed;
    public event Action Died;
    public UnityEvent onDied;                // optional Inspector wiring

    float nextHitTime;
    bool dead;

    void Awake() => Current = maxHealth;

    public void TakeDamage(int amount)
    {
        if (dead || Time.time < nextHitTime) return;
        nextHitTime = Time.time + invulnSeconds;
        Current = Mathf.Max(0, Current - amount);
        Damaged?.Invoke(Current, maxHealth);
        if (Current == 0)
        {
            dead = true;
            Died?.Invoke();
            onDied.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (dead) return;
        Current = Mathf.Min(maxHealth, Current + amount);
        Healed?.Invoke(Current, maxHealth);
    }
}