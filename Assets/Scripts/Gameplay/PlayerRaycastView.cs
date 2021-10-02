using UnityEngine;

public class PlayerRaycastView : MonoBehaviour
{
    [SerializeField] Transform playerView;

    [Header("Var")]
    public float rayDist;
    public Bounds bounds;
    public LayerMask layerMask = 1;

    [Header("Debug")]
    public bool drawGizmo;
    RaycastHit hit;
    [SerializeField] GameObject[] collidersHit;
    RaycastHit[] m_Hit = new RaycastHit[0];

    void FixedUpdate()
    {
        bool hitted = Physics.Raycast(playerView.position, playerView.forward, out hit, bounds.extents.z * 2, layerMask);
        if (hitted)
        {
            Debug.DrawRay(playerView.position, playerView.forward * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(playerView.position, playerView.forward * bounds.extents.z, Color.red);
        }


        // Fat Raycast
        
        m_Hit = Physics.BoxCastAll(playerView.position, bounds.extents, playerView.forward, playerView.rotation, bounds.extents.z, layerMask);
        collidersHit = new GameObject[m_Hit.Length];
        for (int i = 0; i < m_Hit.Length; i++)
        {
            collidersHit[i] = m_Hit[i].collider.gameObject;
        }
    }

    void OnDrawGizmos()
    {
        if (!drawGizmo) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < collidersHit.Length; i++)
        {
            Gizmos.DrawWireSphere(collidersHit[i].transform.position, 15f);
        }

        if (m_Hit.Length > 0) // HIT
        {
            Gizmos.color = Color.green;
        }
        else // NO HIT
        {
            Gizmos.color = Color.red;
        }

        //Gizmos.DrawRay(playerView.position, playerView.forward * maxDistance);
        ExtDebug.DrawBoxCastOnHit(playerView.position, bounds.extents, playerView.rotation, playerView.forward, bounds.extents.z, Gizmos.color);
    }
}
