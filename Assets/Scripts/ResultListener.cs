using System.Collections;
using TMPro;
using UnityEngine;

public class ResultListener : MonoBehaviour
{
    [SerializeField] GameFlow session;
    [SerializeField] GameObject resultPanel;
    [SerializeField] TMP_Text title;
    [SerializeField] GameObject pauseButton;          // optional: result ke waqt chhupa do
    [SerializeField] Color winColor = Color.cyan;
    [SerializeField] Color loseColor = new Color(1f, 0.3f, 0.6f);
    [SerializeField] float showDelay = 0.6f;          // taaki death/win sound pehle sunai de

    void OnEnable() { session.GameWon += OnWon; session.GameLost += OnLost; }
    void OnDisable() { session.GameWon -= OnWon; session.GameLost -= OnLost; }

    void OnWon() => StartCoroutine(Show("PACKET DELIVERED", winColor));
    void OnLost() => StartCoroutine(Show("PACKET LOST", loseColor));

    IEnumerator Show(string text, Color color)
    {
        if (pauseButton) pauseButton.SetActive(false);
        yield return new WaitForSecondsRealtime(showDelay);
        title.text = text;
        title.color = color;
        resultPanel.SetActive(true);
        Time.timeScale = 0f;
        if (UiAudio.Instance) UiAudio.Instance.Open();
    }
}