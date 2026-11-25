using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UiButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public enum Kind { Click, Back, Success, Error }
    [SerializeField] Kind pressSound = Kind.Click;
    [SerializeField] bool playHover = true;
    Button button;

    void Awake() => button = GetComponent<Button>();
    void OnEnable() => button.onClick.AddListener(OnPress);
    void OnDisable() => button.onClick.RemoveListener(OnPress);

    void OnPress()
    {
        var ui = UiAudio.Instance;
        if (ui == null) return;
        switch (pressSound)
        {
            case Kind.Back: ui.Back(); break;
            case Kind.Success: ui.Success(); break;
            case Kind.Error: ui.Error(); break;
            default: ui.Click(); break;
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (playHover && button.interactable && UiAudio.Instance != null)
            UiAudio.Instance.Hover();
    }
}