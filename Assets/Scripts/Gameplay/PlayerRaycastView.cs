using UnityEngine;

public class PlayerRaycastView : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Transform playerView;

    [Header("Var")]
    public float rayDist;
    public Bounds bounds;
    public LayerMask layerMask = 1;

    [Header("Debug")]
    public bool drawGizmo;
    [SerializeField] PhotoTarget target, newTarget;
    [SerializeField] GameObject[] collidersHit;

    RaycastHit hit;
    RaycastHit[] m_Hit = new RaycastHit[0];

    private void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    void FixedUpdate()
    {
        bool hitted = Physics.Raycast(playerView.position, playerView.forward, out hit, bounds.extents.z * 10, layerMask);
        if (hitted && hit.collider.gameObject.TryGetComponent<PhotoTarget>(out PhotoTarget photoTarget))
        {
            if (photoTarget.info && photoTarget.info.important) newTarget = photoTarget;
            else newTarget = null;
        }
        else
        {
            newTarget = null;
        }

        if(newTarget != target)
        {
            target = newTarget;
            gameManager.uiTarget.UpdateTarget(target);
        }

    }

    public void DoBoxCast()
    {
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
