using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Screenshot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public ScreenshotData data;
    [SerializeField] bool clickable;
    [SerializeField] UI_Gallery parent;

    [SerializeField] RawImage img;
    [SerializeField] Image selector;
    [SerializeField] TextMeshProUGUI number, score;
    public void Init(UI_Gallery parent, ScreenshotData data)
    {
        this.parent = parent;
        this.data = data;

        img.texture = data.tex;

        if(number) number.text = "#" + data.ID.ToString("D3");
        if(score) score.text = "Rating - " + data.score;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clickable) parent.ShowBigger(data);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (clickable && selector) selector.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (clickable && selector) selector.enabled = false;
    }
}
