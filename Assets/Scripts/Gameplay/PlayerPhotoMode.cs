using UnityEngine;
using Cinemachine;

public class PlayerPhotoMode : MonoBehaviour
{
    public CinemachineVirtualCamera photoModeCamera;
    public CanvasGroup photoModeCanvas;
    public Vector2 focal;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FreezeTime();
        }
    }

    void FreezeTime()
    {
        if (Time.timeScale > 0.5f) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void ChangeFocal(float value)
    {
        float newFOV = Mathf.Lerp(focal.x, focal.y, value);
        photoModeCamera.m_Lens.FieldOfView = newFOV;
    }

    public void ViewRoll(float slider)
    {
        //photoModeCamera.m_Lens.Dutch = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue) * (viewRoll.max - viewRoll.min) + viewRoll.min;
    }
}
