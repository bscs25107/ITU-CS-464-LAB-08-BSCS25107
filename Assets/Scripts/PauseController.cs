using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameFlow flow;

    bool paused;
    bool settingsFromPause;

    void Awake()
    {
        if (!pausePanel) Debug.LogError("PauseController: Pause Panel not assigned", this);
        if (!settingsPanel) Debug.LogError("PauseController: Settings Panel not assigned", this);
    }

    void Freeze() { paused = true; Time.timeScale = 0f; AudioListener.pause = true; }
    void Unfreeze() { paused = false; Time.timeScale = 1f; AudioListener.pause = false; }
    static void Show(GameObject g, bool on) { if (g) g.SetActive(on); }
    bool Over => flow && flow.IsOver;

    public void Pause()
    {
        if (paused || Over) return;
        Freeze();
        if (UiAudio.Instance) UiAudio.Instance.Pause();
        Show(pausePanel, true);
    }

    public void Resume()
    {
        if (!paused) return;
        Unfreeze();
        if (UiAudio.Instance) UiAudio.Instance.Resume();
        Show(pausePanel, false);
        Show(settingsPanel, false);
    }

    public void OpenSettings()
    {
        if (Over) return;
        settingsFromPause = paused;          // opened from the pause menu?
        if (!paused) Freeze();
        Show(pausePanel, false);
        Show(settingsPanel, true);
    }

    public void CloseSettings()
    {
        Show(settingsPanel, false);
        if (settingsFromPause) Show(pausePanel, true);   // back to pause menu
        else Unfreeze();                                 // opened from HUD: resume game
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}