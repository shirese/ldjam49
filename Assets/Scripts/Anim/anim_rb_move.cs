using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class anim_rb_move : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
        //rb.AddForce(transform.forward * rb.mass * speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5000);
    }
}
