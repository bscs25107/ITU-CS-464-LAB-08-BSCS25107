// 4. Camera shake: ShakeOnHit.cs  (put the camera under an empty "CameraRig" and shake the child)
using System.Collections;
using UnityEngine;

public class ShakeonHit : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] Transform cam;
    [SerializeField] float magnitude = 0.15f, duration = 0.3f;
    Vector3 origin;
    Coroutine routine;

    void Awake() => origin = cam.localPosition;
    void OnEnable() => health.Damaged += OnDamaged;
    void OnDisable() { health.Damaged -= OnDamaged; if (cam) cam.localPosition = origin; }

    void OnDamaged(int c, int m)
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float fade = 1f - t / duration;
            cam.localPosition = origin + (Vector3)Random.insideUnitCircle * magnitude * fade;
            yield return null;
        }
        cam.localPosition = origin;
    }
}