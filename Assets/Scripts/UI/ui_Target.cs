using UnityEngine;
using TMPro;

public class ui_Target : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Canvas _canvas;
    RectTransform canvasRect;

    [Header("Infos")]
    [SerializeField] RectTransform rTransform;
    [SerializeField] TextMeshProUGUI tmp;

    [Header("Target")]
    public PhotoTarget target;

    void Awake()
    {
        canvasRect = _canvas.GetComponent<RectTransform>();
        UpdateTarget(null);
    }

    void Update()
    {
        if(target)
        {
            Vector2 ViewportPosition = cam.WorldToViewportPoint(target.transform.position);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
            rTransform.anchoredPosition = WorldObject_ScreenPosition;
        }
    }
    public void UpdateTarget(PhotoTarget target)
    {
        this.target = target;
        if (target && target.info) tmp.text = target.info.publicName;
        else rTransform.anchoredPosition = Vector2.one * Screen.width*2;
    }
}
