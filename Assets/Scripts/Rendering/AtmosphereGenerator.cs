using UnityEngine;

[ExecuteAlways]
public class AtmosphereGenerator : MonoBehaviour
{
    public AtmosphereSettings Settings;
    public Material AtmosphereMaterial;
    public float bodyRadius;

    void Start()
    {

    }

    void Update()
    {
        if (AtmosphereMaterial != null) Settings.SetProperties(AtmosphereMaterial, bodyRadius);
    }
}
