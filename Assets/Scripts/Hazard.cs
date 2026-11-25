using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] int damage = 20;
    [SerializeField] float cooldown = 1f;
    float next;

    void OnCollisionStay(Collision c)
    {
        if (Time.time < next) return;
        if (c.collider.TryGetComponent(out Health h))
        {
            h.Damage(damage);
            next = Time.time + cooldown;
        }
    }
}