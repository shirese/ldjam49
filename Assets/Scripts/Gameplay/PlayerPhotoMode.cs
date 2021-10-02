using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PlayerPhotoMode : MonoBehaviour
{
    public CinemachineVirtualCamera photoModeCamera;
    public CanvasGroup photoModeCanvas;

    [Header("Ranges")]
    public Vector2 fov;
    public float camAngle;
    public float exposure;
    public Canvas frames;

    // VOLUME
    [Header("PostProcess")]
    [SerializeField] private Volume photoModeVolume;
    private VolumeProfile photoModeVolumeProfile;
    private ColorAdjustments colorAdj;
    private DepthOfField dof;

    //Storage
    private Tween canvasAnim;
    private GameObject[] frameArray;
    private int actualFrame = 0;

    private void Awake()
    {
        InitPostProcess();
        InitFrames();

        photoModeCanvas.alpha = 0;
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
    }

    void FreezeTime()
    {
        canvasAnim.Complete();

        if (Time.timeScale > 0.5f) // SHOW PHOTO MODE
        {
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
        photoModeCamera.m_Lens.FieldOfView = newFOV;
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

    public void NextFrame()
    {
        actualFrame++;
        if (actualFrame >= frameArray.Length) actualFrame = 0;

        for (int i = 0; i < frameArray.Length; i++)
        {
            frameArray[i].SetActive(i == actualFrame);
        }
    }
}
