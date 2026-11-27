using System.Collections;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] LevelObjective objective;
    [SerializeField] TMP_Text hint;
    [SerializeField] GameFlow flow;
    [SerializeField] float hintSeconds = 2.5f;

    Coroutine hideRoutine;

    void Awake()
    {
        if (!flow) flow = FindFirstObjectByType<GameFlow>();
        if (!hint) Debug.LogWarning("Goal: hint text is not assigned", this);
        else hint.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!objective.Complete)
        {
            ShowHint($"Collect all spheres first ({objective.Collected}/{objective.Total})");
            if (UiAudio.Instance) UiAudio.Instance.Error();
            return;
        }

        Debug.Log("Level won");
        if (flow) flow.Win();
    }

    void ShowHint(string msg)
    {
        Debug.Log(msg);
        if (!hint) return;
        hint.gameObject.SetActive(true);
        hint.text = msg;
        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(ClearAfterDelay());
    }

    IEnumerator ClearAfterDelay()
    {
        yield return new WaitForSecondsRealtime(hintSeconds);
        hint.text = "";
    }
}