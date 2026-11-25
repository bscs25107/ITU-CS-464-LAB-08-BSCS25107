using UnityEngine;

public class UiAudio : OneShotPlayer
{
    public static UiAudio Instance { get; private set; }

    [SerializeField] AudioClip hover, click, back, open, close, pause, resume, success, error;

    void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        foreach (var s in sources) s.ignoreListenerPause = true;
    }

    public void Hover() => Play(hover, 0.6f, 0f);
    public void Click() => Play(click, 1f, 0.3f);
    public void Back() => Play(back, 1f, 0.3f);
    public void Open() => Play(open, 1f, 0f);
    public void Close() => Play(close, 1f, 0f);
    public void Pause() => Play(pause, 1f, 0f);
    public void Resume() => Play(resume, 1f, 0f);
    public void Success() => Play(success, 1f, 0f);
    public void Error() => Play(error, 1f, 0f);
}