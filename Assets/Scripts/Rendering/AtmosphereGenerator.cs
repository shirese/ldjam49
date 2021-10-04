using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        if (AtmosphereMaterial != null)
            Settings.SetProperties(AtmosphereMaterial, bodyRadius);
    }
}
