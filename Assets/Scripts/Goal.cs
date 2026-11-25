using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] LevelObjective objective;
    [SerializeField] TMP_Text hint;   // optional

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!objective.Complete)
        {
            string msg = $"Collect all spheres first ({objective.Collected}/{objective.Total})";
            Debug.Log(msg);
            if (hint != null) hint.text = msg;
            return;
        }

        Debug.Log("Level won");   // Part D mein yahan Won() aayega
    }
}