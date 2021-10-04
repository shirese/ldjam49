using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

[CreateAssetMenu (menuName = "Atmosphere")]
public class AtmosphereSettings : ScriptableObject {

    public bool enabled = true;
    public Shader atmosphereShader;
    public ComputeShader opticalDepthCompute;
    public int textureSize = 256;

    public int inScatteringPoints = 10;
    public int opticalDepthPoints = 10;
    public float densityFalloff = 0.25f;

    public Vector3 wavelengths = new Vector3 (700, 530, 460);

    public Vector4 testParams = new Vector4 (7, 1.26f, 0.1f, 3);
    public float scatteringStrength = 20;
    public float intensity = 1;

    public float ditherStrength = 0.8f;
    public float ditherScale = 4;
    public Texture2D blueNoise;
    public Texture2D opticalDepthTextureBackup;

    [Range (0, 1)]
    public float atmosphereScale = 0.5f;

    public float sunDst = 1;

    RenderTexture opticalDepthTexture;
    bool settingsUpToDate;

    public void SetProperties (Material material, float bodyRadius) {
        if (!settingsUpToDate || !Application.isPlaying) {
            float atmosphereRadius = (1 + atmosphereScale) * bodyRadius;

            material.SetVector ("params", testParams);
            material.SetInt ("numInScatteringPoints", inScatteringPoints);
            material.SetInt ("numOpticalDepthPoints", opticalDepthPoints);
            material.SetFloat ("atmosphereRadius", atmosphereRadius);
            material.SetFloat ("planetRadius", bodyRadius);
            material.SetFloat ("densityFalloff", densityFalloff);

            // Strength of (rayleigh) scattering is inversely proportional to wavelength^4
            float scatterX = Pow (400 / wavelengths.x, 4);
            float scatterY = Pow (400 / wavelengths.y, 4);
            float scatterZ = Pow (400 / wavelengths.z, 4);
            material.SetVector ("scatteringCoefficients", new Vector3 (scatterX, scatterY, scatterZ) * scatteringStrength);
            material.SetVector ("dirToSun", RenderSettings.sun.transform.GetChild(0).transform.position.normalized);
            material.SetFloat ("intensity", intensity);
            material.SetFloat ("ditherStrength", ditherStrength);
            material.SetFloat ("ditherScale", ditherScale);
            material.SetTexture ("_BlueNoise", blueNoise);

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            PrecomputeOutScattering ();
            material.SetTexture ("_BakedOpticalDepth", opticalDepthTexture);
#endif
            /*
#if UNITY_STANDALONE_WIN
            void SaveTexture () {
                byte[] bytes = toTexture2D(opticalDepthTexture).EncodeToPNG();
                string texturePath = System.IO.Path.Combine(Application.persistentDataPath, "SavedScreen.png");
                System.IO.File.WriteAllBytes(texturePath, bytes);
            }
            Texture2D toTexture2D(RenderTexture rTex)
            {
                Texture2D tex = new Texture2D(256, 256, TextureFormat.RGBAHalf, false);
                RenderTexture.active = rTex;
                tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
                tex.Apply();
                return tex;
            }
            SaveTexture();
#endif
            */
#if UNITY_WEBGL
            //material.SetTexture ("_BakedOpticalDepth", opticalDepthTextureBackup);
#endif
            material.SetTexture("_BakedOpticalDepth", opticalDepthTextureBackup);

            settingsUpToDate = true;
        }
    }

    void PrecomputeOutScattering () {
        if (!settingsUpToDate || opticalDepthTexture == null || !opticalDepthTexture.IsCreated ()) {
            ComputeHelper.CreateRenderTexture (ref opticalDepthTexture, textureSize, FilterMode.Bilinear);
            opticalDepthCompute.SetTexture (0, "Result", opticalDepthTexture);
            opticalDepthCompute.SetInt ("textureSize", textureSize);
            opticalDepthCompute.SetInt ("numOutScatteringSteps", opticalDepthPoints);
            opticalDepthCompute.SetFloat ("atmosphereRadius", (1 + atmosphereScale));
            opticalDepthCompute.SetFloat ("densityFalloff", densityFalloff);
            opticalDepthCompute.SetVector ("params", testParams);
            ComputeHelper.Run (opticalDepthCompute, textureSize, textureSize);
        }

    }

    void OnValidate () {
        settingsUpToDate = false;
    }
}