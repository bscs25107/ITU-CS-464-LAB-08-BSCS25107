// 2. Sound: DamageSfx.cs
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] PlayerHealth health;
    [SerializeField] AudioClip hurt, heal, death;

    void OnEnable() { health.Damaged += OnDamaged; health.Healed += OnHealed; health.Died += OnDied; }
    void OnDisable() { health.Damaged -= OnDamaged; health.Healed -= OnHealed; health.Died -= OnDied; }

    void OnDamaged(int c, int m) { if (c > 0) SfxPlayer.Instance.Play(hurt); }
    void OnHealed(int c, int m) => SfxPlayer.Instance.Play(heal);
    void OnDied() => SfxPlayer.Instance.Play(death);
}