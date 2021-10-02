using UnityEngine;
using System.Collections.Generic;

public class VisibleElements : MonoBehaviour
{
    public bool drawGizmo;
    public List<PhotoTarget> visibleElements;

    private void Awake()
    {
        visibleElements = new List<PhotoTarget>();
    }

    public void AddElement(PhotoTarget target)
    {
        visibleElements.Add(target);
    }

    public void RemoveElement(PhotoTarget target)
    {
        visibleElements.Remove(target);
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmo) return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < visibleElements.Count; i++)
        {
            if(visibleElements[i])
            {
                Gizmos.DrawWireSphere(visibleElements[i].transform.position, 15f);
            }
        }
    }
}
