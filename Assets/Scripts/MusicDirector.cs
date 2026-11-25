// 6. Music transition + result sounds: MusicDirector.cs
using UnityEngine;

public class MusicDirector : MonoBehaviour
{
    [SerializeField] GameFlow session;
    [SerializeField] AudioClip gameplayMusic, resultsMusic, winSfx, loseSfx;

    void OnEnable()
    {
        session.GameStarted += OnStarted;
        session.GameWon += OnWon;
        session.GameLost += OnLost;
    }
    void OnDisable()
    {
        session.GameStarted -= OnStarted;
        session.GameWon -= OnWon;
        session.GameLost -= OnLost;
    }

    void OnStarted() => MusicPlayer.Instance.Crossfade(gameplayMusic, 1.5f);
    void OnWon() { SfxPlayer.Instance.Play(winSfx); MusicPlayer.Instance.Crossfade(resultsMusic, 1f); }
    void OnLost() { SfxPlayer.Instance.Play(loseSfx); MusicPlayer.Instance.Crossfade(resultsMusic, 1f); }
}