using System;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] Health player;                 // the Health on the Player object

    public event Action GameStarted, GameWon, GameLost;
    public event Action<int, int> Collected;        // count, target

    public Health Player => player;
    public int Count { get; private set; }
    public int Target { get; private set; }
    public bool IsOver { get; private set; }

    void Awake()
    {
        Pickup.Score = 0;                           // static, so reset on every scene start
        Target = FindObjectsByType<Pickup>(FindObjectsSortMode.None).Length;
    }

    void OnEnable()
    {
        Pickup.Collected += OnPickup;
        if (player) player.Died += Lose;
    }

    void OnDisable()
    {
        Pickup.Collected -= OnPickup;               // static event: must unsubscribe
        if (player) player.Died -= Lose;
    }

    void Start()
    {
        if (!player) Debug.LogError("GameFlow: Player (Health) not assigned, Lose will never fire", this);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameStarted?.Invoke();
    }

    void OnPickup(Pickup p)
    {
        if (IsOver) return;
        Count++;
        Collected?.Invoke(Count, Target);
    }

    public void Win() { if (IsOver) return; IsOver = true; Debug.Log("GameFlow: WIN"); GameWon?.Invoke(); }
    public void Lose() { if (IsOver) return; IsOver = true; Debug.Log("GameFlow: LOSE"); GameLost?.Invoke(); }
}