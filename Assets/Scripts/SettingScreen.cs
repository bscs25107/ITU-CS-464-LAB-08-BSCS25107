using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    public ToggleSwitch fxToggle, vibrationToggle;
    public Slider volumeSlider;
    public TMP_Text volumeLabel;
    public CRTOverlay crt;

    void Awake()
    {
        fxToggle.Changed += on => { GameSession.fx = on; crt.SetEffects(on); };
        vibrationToggle.Changed += on => GameSession.vibration = on;
        volumeSlider.onValueChanged.AddListener(v =>
        {
            GameSession.volume = v;
            AudioListener.volume = v;
            volumeLabel.text = Mathf.RoundToInt(v * 100f) + "%";
        });
    }

    void OnEnable()
    {
        fxToggle.Set(GameSession.fx, true);
        vibrationToggle.Set(GameSession.vibration, true);
        volumeSlider.SetValueWithoutNotify(GameSession.volume);
        volumeLabel.text = Mathf.RoundToInt(GameSession.volume * 100f) + "%";
    }
}