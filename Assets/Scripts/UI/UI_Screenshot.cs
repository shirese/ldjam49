using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Screenshot : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI number, score;

    public ScreenshotData data;
    public void Init(int index, ScreenshotData data)
    {
        this.data = data;
        img.sprite = Sprite.Create(data.tex, new Rect(0.0f, 0.0f, data.tex.width, data.tex.height), new Vector2(0.5f, 0.5f));
        number.text = "#" + index.ToString("D3");
        score.text = "Rating - " + data.score;
    }
}
