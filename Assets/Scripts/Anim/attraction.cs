using UnityEngine;
using System.Collections.Generic;

public class attraction : MonoBehaviour
{
    public float attractionForce = 10;
    [SerializeField] List<Rigidbody> rbList;
    Transform _tr;

    void Awake()
    {
        _tr = transform;
    }

    void FixedUpdate()
    {
        foreach(Rigidbody rb in rbList)
        {
            Vector3 force = _tr.position - rb.position;
            rb.AddForce(force.normalized * attractionForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rbList.Add(rb);
        }
    }
}
