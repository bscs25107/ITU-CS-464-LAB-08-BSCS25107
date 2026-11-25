using System;
using UnityEngine;

public class LevelObjective : MonoBehaviour
{
    public int Total { get; private set; }
    public int Collected { get; private set; }
    public bool Complete => Collected >= Total;
    public event Action<int, int> Changed;   // (collected, total)

    void Awake()
    {
        Pickup.Score = 0;   // static hai, load ke baad bhi bacha rehta hai
        Total = FindObjectsByType<Pickup>(FindObjectsSortMode.None).Length;
    }

    void OnEnable() => Pickup.Collected += OnCollected;
    void OnDisable() => Pickup.Collected -= OnCollected;

    void OnCollected(Pickup p)
    {
        Collected++;
        Changed?.Invoke(Collected, Total);
    }
}