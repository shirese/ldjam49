using UnityEngine;

public class animRotationAxis : MonoBehaviour
{
    public bool x, y, z;
    public float speed;

    [Header("Gizmos")]
    public bool showGizmo;
    public int radius = 250;
    
    void Update()
    {
        if (x)
            transform.Rotate(speed * Time.deltaTime, 0, 0, Space.Self);

        if (y)
            transform.Rotate(0, speed * Time.deltaTime, 0, Space.Self);

        if (z)
            transform.Rotate(0, 0, speed * Time.deltaTime, Space.Self);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, radius);
        }
    }
}
