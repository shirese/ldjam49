using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class animMaterialOfsset : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Vector2 offsetSpeed;

    void Awake()
    {
        MeshRenderer r = GetComponent<MeshRenderer>();
        if (r) mat = r.material;
    }

    void Update()
    {
        mat.mainTextureOffset = Time.timeSinceLevelLoad * offsetSpeed;
    }
}
