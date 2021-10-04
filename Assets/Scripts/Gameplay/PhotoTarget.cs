using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PhotoTarget : MonoBehaviour
{
    [SerializeField] Collider _collider;

    [Header("Infos")]
    public bool alreadyTaken;
    public PhotoTargetInfo info;

    void Awake()
    {
        alreadyTaken = false;
        if(!_collider) _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    public void TakenInPicture()
    {
        alreadyTaken = true;
    }
}
