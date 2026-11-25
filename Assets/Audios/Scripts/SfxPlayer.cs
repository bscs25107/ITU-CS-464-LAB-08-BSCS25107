public class SfxPlayer : OneShotPlayer
{
    public static SfxPlayer Instance { get; private set; }
    void Awake() { if (Instance == null) Instance = this; }
}