using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] AudioClip menuMusic;

    void Start()
    {
        if (menuMusic == null) { Debug.LogError("MenuMusic: no clip assigned", this); return; }
        if (MusicPlayer.Instance == null) { Debug.LogError("MenuMusic: MusicPlayer.Instance is null (AudioRoot missing or Music object disabled)", this); return; }
        MusicPlayer.Instance.Crossfade(menuMusic, 0.5f);
        Debug.Log("MenuMusic: crossfading to " + menuMusic.name);
    }
}