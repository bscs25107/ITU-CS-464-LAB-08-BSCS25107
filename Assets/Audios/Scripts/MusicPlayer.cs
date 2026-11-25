using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] AudioSource a, b;
    [SerializeField] float defaultFade = 1.5f;
    AudioSource active, idle;
    Coroutine fade;

    void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        foreach (var s in new[] { a, b })
        {
            s.loop = true;
            s.playOnAwake = false;
            s.volume = 0f;
            s.ignoreListenerPause = true;   // music keeps playing while paused
        }
        active = a; idle = b;
    }

    public void Crossfade(AudioClip clip, float duration = -1f)
    {
        if (clip == null || (active.clip == clip && active.isPlaying)) return;
        if (duration < 0f) duration = defaultFade;
        if (fade != null)
        {
            StopCoroutine(fade);
            if (idle.volume > active.volume) (active, idle) = (idle, active);
            idle.Stop(); idle.volume = 0f;
        }
        fade = StartCoroutine(Fade(clip, duration));
    }

    IEnumerator Fade(AudioClip clip, float duration)
    {
        AudioSource from = active, to = idle;
        float startVol = from.volume;
        to.clip = clip; to.volume = 0f; to.Play();

        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            float k = t / duration;
            from.volume = startVol * Mathf.Cos(k * Mathf.PI * 0.5f);   // equal power
            to.volume = Mathf.Sin(k * Mathf.PI * 0.5f);
            yield return null;
        }
        from.Stop(); from.volume = 0f; to.volume = 1f;
        active = to; idle = from; fade = null;
    }
}