using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using PhotoMode;
using TMPro;

public class PlayerPhotoMode : MonoBehaviour
{
    public CinemachineVirtualCamera photoModeCamera;
    public CinemachineVirtualCamera exteriorCamera;

    [Header("Scene Elements")]
    [SerializeField] CanvasGroup photoModeCanvas;
    [SerializeField] Canvas frames;
    [SerializeField] GameEvent showUI;
    [SerializeField] TextMeshProUGUI zoomLevelText;

    [Header("Ranges")]
    public Vector2 fov;
    public float camAngle;
    public float exposure;

    // VOLUME
    [Header("PostProcess")]
    [SerializeField] private Volume photoModeVolume;
    private VolumeProfile photoModeVolumeProfile;
    private ColorAdjustments colorAdj;
    private DepthOfField dof;

    [Header("Shaders")]
    [SerializeField] Material postProcessingMaterial;
    [SerializeField] Blit blit;
    [SerializeField] Shader[] shaderArray;

    //Storage
    private Tween canvasAnim;
    private GameObject[] frameArray;
    private int actualFrame = 0;
    private int actualShader = 0;

    private void Awake()
    {
        InitPostProcess();
        InitFrames();
        postProcessingMaterial.shader = shaderArray[0];

        photoModeCanvas.alpha = 0;
    }

    private void OnApplicationQuit()
    {
        postProcessingMaterial.shader = shaderArray[0];
    }

    void InitFrames()
    {
        Transform fTransform = frames.transform;
        frameArray = new GameObject[fTransform.childCount];
        actualFrame = 0;

        for (int i = 0; i < fTransform.childCount; i++)
        {
            frameArray[i] = fTransform.GetChild(i).gameObject;
            frameArray[i].SetActive(i == actualFrame);
        }
    }

    public void SwitchCamera()
    {
        Debug.Log("SWITCH CAMERA");
        photoModeCamera.m_Priority = 10;

        if (exteriorCamera.m_Priority >= 10) exteriorCamera.m_Priority = 5;
        else exteriorCamera.m_Priority = 15;
    }

    void InitPostProcess()
    {
        photoModeVolumeProfile = photoModeVolume.profile;
        photoModeVolumeProfile.TryGet<DepthOfField>(out dof);
        photoModeVolumeProfile.TryGet<ColorAdjustments>(out colorAdj);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FreezeTime();
        }

        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            ScrollFocal(scroll);
        }
    }

    void FreezeTime()
    {
        canvasAnim.Complete();

        if (Time.timeScale > 0.5f) // SHOW PHOTO MODE
        {
            showUI.Raise();
            canvasAnim = photoModeCanvas.DOFade(1, 0.2f).SetUpdate(true);
            Time.timeScale = 0;
        }
        else // HIDE PHOTO MODE
        {
            Time.timeScale = 1;
            canvasAnim = photoModeCanvas.DOFade(0, 0.2f);
            ResetParams();
        }
    }

    private void ResetParams()
    {
        ViewRoll(0.5f);
    }

    public void ChangeFocal(float value)
    {
        float newFOV = Mathf.Lerp(fov.x, fov.y, value);
        SetFocal(newFOV);
    }
    public void ScrollFocal(float value)
    {
        float newFOV = Mathf.Clamp(photoModeCamera.m_Lens.FieldOfView - value, fov.x, fov.y);
        SetFocal(newFOV);
    }

    void SetFocal(float newFOV)
    {
        photoModeCamera.m_Lens.FieldOfView = newFOV;
        string text = newFOV == fov.x ? "MAX" : (fov.y - newFOV).ToString();
        if (zoomLevelText) zoomLevelText.text = "ZOOM LEVEL\n" + text;
    }

    public void ViewRoll(float value)
    {
        photoModeCamera.m_Lens.Dutch = Mathf.Lerp(-camAngle, camAngle, value);
    }
    public void Exposure(float value)
    {
        colorAdj.postExposure.Override(Mathf.Lerp(-exposure, exposure, value));
    }

    public void Aperture(float value)
    {
        //dof.aperture.Override((slider.value - slider.minValue) / (slider.maxValue - slider.minValue) * (aperture.max - aperture.min) + aperture.min);
    }

    public void ChangeFrame(int value)
    {
        actualFrame += value;
        if (actualFrame >= frameArray.Length) actualFrame = 0;
        else if (actualFrame < 0) actualFrame = frameArray.Length - 1;

        for (int i = 0; i < frameArray.Length; i++)
        {
            frameArray[i].SetActive(i == actualFrame);
        }
    }

    public void ChangeFilter(int value)
    {
        if (blit != null) blit.SetActive(true);

        actualShader += value;
        if (actualShader >= shaderArray.Length) actualShader = 0;
        else if (actualShader < 0) actualShader = shaderArray.Length - 1;

        Shader shader = shaderArray[actualShader];
        postProcessingMaterial.shader = shader;
    }
}
