using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] string parameterName = "MusicVolume";
    [SerializeField] Toggle muteToggle;      // optional, isOn = muted
    [SerializeField] TMP_Text valueText;     // optional, shows 0-100%
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0.0001f;
        slider.maxValue = 1f;
    }

    void OnEnable()
    {
        slider.SetValueWithoutNotify(AudioSettingsManager.GetVolume(parameterName));
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(OnSlider);
        if (muteToggle)
        {
            muteToggle.SetIsOnWithoutNotify(AudioSettingsManager.GetMuted(parameterName));
            muteToggle.onValueChanged.AddListener(OnMute);
        }
    }

    void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSlider);
        if (muteToggle) muteToggle.onValueChanged.RemoveListener(OnMute);
    }

    void OnSlider(float v)
    {
        AudioSettingsManager.SetVolume(parameterName, v);
        UpdateText(v);
    }

    void OnMute(bool m) => AudioSettingsManager.SetMuted(parameterName, m);

    void UpdateText(float v)
    {
        if (valueText) valueText.text = Mathf.RoundToInt(v * 100f) + "%";
    }
}