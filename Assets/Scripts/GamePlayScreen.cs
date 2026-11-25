using TMPro;
using UnityEngine;

public class GamePlayScreen : MonoBehaviour
{
    public TMP_Text scoreText;
    float score;

    void OnEnable() { ResetRun(); }

    public void ResetRun()
    {
        score = 0f;
        Refresh();
    }

    void Update()
    {
        if (PauseScreen.IsPaused) return;

        score += Time.deltaTime * 10f;       // timeScale 0 par ye khud ruk jata hai
        Refresh();

        if (Input.GetKeyDown(KeyCode.Escape))
            ScreenManager.I.Open("Pause");
    }

    void Refresh()
    {
        scoreText.text = "SIGNAL " + Mathf.FloorToInt(score).ToString("000");
    }
    public void Finish()
    {
        GameSession.score = Mathf.FloorToInt(score);
        ScreenManager.I.Open("Results");
    }
}