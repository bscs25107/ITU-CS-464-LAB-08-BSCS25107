using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int max = 100;
    [SerializeField] int current;      // Inspector mein dikhega
    public int Current => current;

    public event Action<int, int> Damaged;
    public event Action Died;

    void Awake() => current = max;

    public void Damage(int amount)
    {
        if (current <= 0) return;
        current = Mathf.Max(0, current - amount);
        Debug.Log($"Health {current}/{max}");
        Damaged?.Invoke(current, max);
        if (current == 0) Died?.Invoke();
    }
}