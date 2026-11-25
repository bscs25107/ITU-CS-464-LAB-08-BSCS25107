using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Guard : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speed = 2.5f;
    [SerializeField] float waitTime = 0.5f;
    Rigidbody rb;
    int index;
    float wait;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        if (points == null || points.Length < 2) return;
        if (wait > 0f) { wait -= Time.fixedDeltaTime; return; }

        Vector3 p = points[index].position;
        Vector3 target = new Vector3(p.x, rb.position.y, p.z);
        Vector3 next = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);

        Vector3 dir = target - rb.position;
        if (dir.sqrMagnitude > 0.0001f)
            rb.MoveRotation(Quaternion.LookRotation(dir));
        else
        {
            index = (index + 1) % points.Length;
            wait = waitTime;
        }
    }
}