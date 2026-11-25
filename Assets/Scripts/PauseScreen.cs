using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    void OnEnable()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    void OnDisable()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}