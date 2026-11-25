using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance { get; private set; }
    public static readonly string[] Parameters =
        { "MasterVolume", "MusicVolume", "SFXVolume", "UIVolume" };

    [SerializeField] AudioMixer mixer;
    const float MinLinear = 0.0001f;   // log10(0) = -infinity

    void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }

    // SetFloat on a mixer is unreliable in Awake, so apply in Start.
    void Start() { foreach (var p in Parameters) Apply(p); }

    public static float ToDecibels(float linear) =>
        Mathf.Log10(Mathf.Clamp(linear, MinLinear, 1f)) * 20f;

    public static float GetVolume(string p) => PlayerPrefs.GetFloat("vol_" + p, 0.8f);
    public static bool GetMuted(string p) => PlayerPrefs.GetInt("mute_" + p, 0) == 1;

    public static void SetVolume(string p, float v)
    {
        PlayerPrefs.SetFloat("vol_" + p, Mathf.Clamp(v, MinLinear, 1f));
        PlayerPrefs.Save();
        if (Instance) Instance.Apply(p);
    }

    public static void SetMuted(string p, bool muted)
    {
        PlayerPrefs.SetInt("mute_" + p, muted ? 1 : 0);   // slider value is kept
        PlayerPrefs.Save();
        if (Instance) Instance.Apply(p);
    }

    void Apply(string p) =>
        mixer.SetFloat(p, GetMuted(p) ? -80f : ToDecibels(GetVolume(p)));
}