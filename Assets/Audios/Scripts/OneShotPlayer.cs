using UnityEngine;

public abstract class OneShotPlayer : MonoBehaviour
{
    [SerializeField] protected AudioSource[] sources;      // 4 sources, same group
    [SerializeField, Range(0f, 4f)] float pitchSemitones = 1.5f;
    int next;

    public void Play(AudioClip clip, float volume = 1f, float semitones = -1f)
    {
        if (clip == null) return;
        var s = sources[next];
        next = (next + 1) % sources.Length;
        float st = semitones < 0f ? pitchSemitones : semitones;
        s.pitch = Mathf.Pow(2f, Random.Range(-st, st) / 12f);
        s.PlayOneShot(clip, volume * Random.Range(0.9f, 1f));
    }
}