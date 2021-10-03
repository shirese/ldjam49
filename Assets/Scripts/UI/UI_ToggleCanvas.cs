using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UI_ToggleCanvas : MonoBehaviour
{
    Canvas _canvas;
    void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Toggle()
    {
        _canvas.enabled = !_canvas.enabled;
    }

    public void Set(bool value)
    {
        _canvas.enabled = value;
    }
}
