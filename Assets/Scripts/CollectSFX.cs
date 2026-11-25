// 5. Pickup sound: CollectSfx.cs
using UnityEngine;

public class CollectSfx : MonoBehaviour
{
    [SerializeField] GameFlow session;
    [SerializeField] AudioClip pickup;
    void OnEnable() => session.Collected += OnCollected;
    void OnDisable() => session.Collected -= OnCollected;
    void OnCollected(int n, int target) => SfxPlayer.Instance.Play(pickup);
}