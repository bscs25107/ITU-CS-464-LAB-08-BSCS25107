using System;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] PlayerHealth player;      // khali chhod sakti ho agar health nahi hai
    [SerializeField] int fragmentsToWin = 5;

    public event Action GameStarted, GameWon, GameLost;
    public event Action<int, int> Collected;

    int collected;
    bool over;

    void OnEnable() { if (player) player.Died += Lose; }
    void OnDisable() { if (player) player.Died -= Lose; }

    void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameStarted?.Invoke();
    }

    public void Collect()
    {
        if (over) return;
        collected++;
        Collected?.Invoke(collected, fragmentsToWin);
        if (fragmentsToWin > 0 && collected >= fragmentsToWin) Win();
    }

    public void Win() { if (over) return; over = true; GameWon?.Invoke(); }
    public void Lose() { if (over) return; over = true; GameLost?.Invoke(); }
}