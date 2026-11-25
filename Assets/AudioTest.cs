using UnityEngine;

public class AudioTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("UiAudio.Instance = " + UiAudio.Instance);
            if (UiAudio.Instance) UiAudio.Instance.Click();
        }
    }
}