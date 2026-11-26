using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameFlow flow;            // optional: result ke baad pause na ho
    bool paused;

    public void Pause()
    {
        if (paused || (flow && flow.IsOver)) return;
        paused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;                       // SFX freeze
        if (UiAudio.Instance) UiAudio.Instance.Pause();   // UI sources pause ignore karte hain
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        if (!paused) return;
        paused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (UiAudio.Instance) UiAudio.Instance.Resume();
        pausePanel.SetActive(false);
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}