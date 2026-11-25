using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] points;     // waypoints (platform ke child nahi)
    [SerializeField] float speed = 2f;
    [SerializeField] float waitTime = 0.5f;

    Rigidbody rb;
    Collider col;
    readonly HashSet<Rigidbody> riders = new HashSet<Rigidbody>();
    int index;
    float wait;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        if (points == null || points.Length < 2) return;
        if (wait > 0f) { wait -= Time.fixedDeltaTime; return; }

        Vector3 target = points[index].position;
        Vector3 next = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        Vector3 delta = next - rb.position;

        rb.MovePosition(next);
        foreach (var r in riders)
            if (r != null) r.position += delta;      // player platform ke saath chalta hai

        if ((target - next).sqrMagnitude < 0.0001f)
        {
            index = (index + 1) % points.Length;
            wait = waitTime;
        }
    }

    void OnCollisionStay(Collision c)
    {
        Rigidbody other = c.rigidbody;
        if (other == null) return;

        // sirf wohi jo platform ke upar khada ho
        bool above = c.collider.bounds.min.y >= col.bounds.max.y - 0.15f;
        if (above) riders.Add(other); else riders.Remove(other);
    }

    void OnCollisionExit(Collision c)
    {
        if (c.rigidbody != null) riders.Remove(c.rigidbody);
    }
}