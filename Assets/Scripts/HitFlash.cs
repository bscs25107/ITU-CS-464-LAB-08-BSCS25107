// 3. Hit flash: HitFlash.cs
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HitFlash : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] Image flash;          // full-screen red, Raycast Target OFF
    Coroutine routine;

    void OnEnable() => health.Damaged += OnDamaged;
    void OnDisable() => health.Damaged -= OnDamaged;

    void OnDamaged(int c, int m)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        const float dur = 0.18f;
        Color col = flash.color;
        for (float t = 0f; t < dur; t += Time.unscaledDeltaTime)
        {
            col.a = Mathf.Lerp(0.5f, 0f, t / dur);
            flash.color = col;
            yield return null;
        }
        col.a = 0f;
        flash.color = col;
    }
}