using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager I;
    public UIScreen[] screens;
    public string startScreen = "Title";
    public CRTOverlay crt;

    Dictionary<string, UIScreen> map = new Dictionary<string, UIScreen>();
    Stack<UIScreen> history = new Stack<UIScreen>();
    UIScreen current;
    bool busy;

    void Awake()
    {
        I = this;
        foreach (var s in screens) { map[s.name] = s; s.gameObject.SetActive(false); }
    }

    void Start() { StartCoroutine(Show(map[startScreen], 1f)); }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && current != null && !current.handlesEscape)
            Back();
    }
    public void Open(string name)
    {
        if (busy) { Debug.Log("ScreenManager busy, ignored: " + name); return; }
        if (!map.ContainsKey(name)) { Debug.LogWarning("Screen not found: " + name); return; }
        StartCoroutine(OpenRoutine(map[name]));
    }
    public void Back()
    {
        if (busy || history.Count == 0) return;
        StartCoroutine(BackRoutine());
    }

    public void GoRoot(string name)
    {
        if (busy || !map.ContainsKey(name)) return;
        history.Clear();
        StartCoroutine(RootRoutine(map[name]));
    }

    IEnumerator Show(UIScreen s, float dir)
    {
        busy = true;
        current = s;
        yield return s.In(dir);
        busy = false;
    }

    IEnumerator OpenRoutine(UIScreen next)
    {
        busy = true;
        history.Push(current);
        if (crt != null) crt.Burst(next.isOverlay ? 0.15f : 0.28f, next.isOverlay ? 0.25f : 0.35f);
        if (!next.isOverlay) yield return current.Out(1f);
        current = next;
        yield return next.In(1f);
        busy = false;
    }

    IEnumerator BackRoutine()
    {
        busy = true;
        var prev = history.Pop();
        bool wasOverlay = current.isOverlay;
        if (crt != null) crt.Burst(wasOverlay ? 0.15f : 0.28f, wasOverlay ? 0.25f : 0.35f);
        yield return current.Out(-1f);
        current = prev;
        if (!wasOverlay) yield return prev.In(-1f);
        busy = false;
    }

    IEnumerator RootRoutine(UIScreen root)
    {
        busy = true;
        if (crt != null) crt.Burst();
        foreach (var s in screens)
            if (s != root && s.gameObject.activeSelf) yield return s.Out(1f);
        current = root;
        yield return root.In(-1f);
        busy = false;
    }
    public void Rewind(string name)   // history me peechhe ja kar us screen tak
    {
        if (busy || !map.ContainsKey(name)) return;
        if (!history.Contains(map[name]))
        {
            Debug.LogWarning("Rewind: " + name + " history me nahi hai");
            return;
        }
        StartCoroutine(RewindRoutine(map[name]));
    }

    IEnumerator RewindRoutine(UIScreen target)
    {
        busy = true;
        if (crt != null) crt.Burst();
        while (history.Peek() != target) history.Pop();
        history.Pop();                                  // target ab current banega
        foreach (var s in screens)
            if (s != target && s.gameObject.activeSelf) yield return s.Out(-1f);
        current = target;
        yield return target.In(-1f);
        busy = false;
    }
}