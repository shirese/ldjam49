using UnityEngine;
using UnityEngine.UI;

public class anim_UI_move : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rTransform;
    [SerializeField] float speed;

    // DEBUG
    float finalPos = 0;

    void Start()
    {
        float finalPos = rTransform.sizeDelta.y * .7f;
    }

    void Update()
    {
        if (rTransform.anchoredPosition.y >= finalPos) return;
        rTransform.anchoredPosition = new Vector2(0, rTransform.anchoredPosition.y + speed * Time.deltaTime);
    }
}
