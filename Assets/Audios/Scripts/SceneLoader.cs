using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Slider bar;
    [SerializeField] TMP_Text percentText;
    [SerializeField] float fillSpeed = 1f;   // max 100% per second

    bool loading;

    public void Load(string sceneName)
    {
        Debug.Log("SceneLoader.Load called: " + sceneName);
        if (loading) return;
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' is not in Build Settings.");
            return;
        }
        StartCoroutine(Run(sceneName));
    }

    IEnumerator Run(string sceneName)
    {
        loading = true;
        Time.timeScale = 1f;
        AudioListener.pause = false;

        bar.value = 0f;
        percentText.text = "0%";
        loadingPanel.SetActive(true);
        yield return null;                       // let the panel draw once

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;         // hold at 0.9

        float shown = 0f;
        while (shown < 1f)
        {
            float real = Mathf.Clamp01(op.progress / 0.9f);
            shown = Mathf.MoveTowards(shown, real, fillSpeed * Time.unscaledDeltaTime);
            bar.value = shown;
            percentText.text = Mathf.RoundToInt(shown * 100f) + "%";
            yield return null;
        }

        if (UiAudio.Instance) UiAudio.Instance.Success();
        yield return new WaitForSecondsRealtime(0.25f);
        op.allowSceneActivation = true;
    }
}