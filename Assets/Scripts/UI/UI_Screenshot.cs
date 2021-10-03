using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Screenshot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public ScreenshotData data;
    [SerializeField] bool clickable;
    [SerializeField] UI_Gallery parent;

    [SerializeField] Image img;
    [SerializeField] Image selector;
    [SerializeField] TextMeshProUGUI number, score;
    public void Init(UI_Gallery parent, ScreenshotData data)
    {
        this.parent = parent;
        this.data = data;

        img.sprite = Sprite.Create(data.tex, new Rect(0.0f, 0.0f, data.tex.width, data.tex.height), new Vector2(0.5f, 0.5f));
        img.preserveAspect = true;

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
