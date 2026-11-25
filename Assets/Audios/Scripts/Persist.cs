using UnityEngine;

public class Persist : MonoBehaviour
{
    static bool exists;
    void Awake()
    {
        if (exists) { Destroy(gameObject); return; }
        exists = true;
        DontDestroyOnLoad(gameObject);
    }
}