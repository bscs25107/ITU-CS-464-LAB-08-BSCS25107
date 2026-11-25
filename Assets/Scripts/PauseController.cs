using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;     // tumhari Lab 07 Pause screen
    bool paused;

    public void Pause()
    {
        if (paused) return;
        paused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;             // SFX freeze
        if (UiAudio.Instance) UiAudio.Instance.Pause();   // UI ignores the pause
        pausePanel.SetActive(true);             // ya apna tween/open function call karo
    }

    public void Resume()
    {
        if (!paused) return;
        paused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (UiAudio.Instance) UiAudio.Instance.Resume();
        pausePanel.SetActive(false);            // ya apna close tween
    }

    void OnDisable()
    {
        // scene chhodte waqt kabhi pause state na atke
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}