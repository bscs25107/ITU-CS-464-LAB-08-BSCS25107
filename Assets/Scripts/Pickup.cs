using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public static int Score;
    public static event Action<Pickup> Collected;

    [SerializeField] int points = 10;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Score += points;
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}