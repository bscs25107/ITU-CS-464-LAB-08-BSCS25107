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
    [SerializeField] float fillSpeed = 1f;   // bar can't move faster than 100% per second

    public void Load(string sceneName) => StartCoroutine(Run(sceneName));

    IEnumerator Run(string sceneName)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        loadingPanel.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;          // hold at 0.9

        float shown = 0f;
        while (shown < 1f)
        {
            float real = Mathf.Clamp01(op.progress / 0.9f);   // progress stops at 0.9
            shown = Mathf.MoveTowards(shown, real, fillSpeed * Time.unscaledDeltaTime);
            bar.value = shown;
            percentText.text = Mathf.RoundToInt(shown * 100f) + "%";
            yield return null;
        }

        if (UiAudio.Instance) UiAudio.Instance.Success();
        yield return new WaitForSecondsRealtime(0.25f);
        op.allowSceneActivation = true;           // enter gameplay only now
    }
}