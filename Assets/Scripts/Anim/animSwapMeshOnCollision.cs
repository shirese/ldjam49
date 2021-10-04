using UnityEngine;

public class animSwapMeshOnCollision : MonoBehaviour
{
    [SerializeField] GameObject[] A, B;
    void Start()
    {
        Set(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("planet"))
        {
            Swap();
        }
    }

    [ContextMenu("Swap")]
    public void Swap()
    {
        Set(false, true);
    }

    void Set(bool state, bool deparent = false)
    {
        foreach (GameObject o in A)
        {
            if (o) o.SetActive(state);
        }

        foreach (GameObject o in B)
        {
            if (o)
            {
                o.SetActive(!state);
                if (deparent) o.transform.SetParent(null);
            }
        }
    }
}
