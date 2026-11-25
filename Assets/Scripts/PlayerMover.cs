using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    [SerializeField] float jump = 6f;
    Rigidbody rb;
    bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        //if (PauseMenu.IsPaused) return;   // PauseMenu Part A mein banta hai
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 v = new Vector3(x, 0, z).normalized * speed;
        rb.linearVelocity = new Vector3(v.x, rb.linearVelocity.y, v.z);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jump, rb.linearVelocity.z);
    }

    void OnCollisionStay(Collision c) => grounded = true;
    void OnCollisionExit(Collision c) => grounded = false;
}