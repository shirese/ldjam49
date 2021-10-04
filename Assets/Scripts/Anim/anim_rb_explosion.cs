using UnityEngine;

public class anim_rb_explosion : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform explosionCenter;
    public float speed = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Vector3 force = explosionCenter.position - transform.position;
        rb.AddForce(force.normalized * rb.mass * speed);
        //rb.AddExplosionForce(rb.mass * speed, explosionCenter.position, 500);
    }
}
