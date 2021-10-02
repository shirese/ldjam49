using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhotoTarget : MonoBehaviour
{
    VisibleElements _visibleElements;
    [SerializeField] Collider _collider;

    [Header("Infos")]
    public bool alreadyTaken;
    public PhotoTargetInfo info;

    void Awake()
    {
        _visibleElements = GetComponentInParent<VisibleElements>();

        alreadyTaken = false;
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    public void TakenInPicture()
    {
        alreadyTaken = true;
    }

    /*
    private void OnBecameVisible()
    {
        if (_visibleElements) _visibleElements.AddElement(this);
    }
    private void OnBecameInvisible()
    {
        if (_visibleElements) _visibleElements.RemoveElement(this);
    }
    */
}
