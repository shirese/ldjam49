using UnityEngine;
using DG.Tweening;

public class UI_ScreenTaken : MonoBehaviour
{
    [SerializeField] CanvasGroup group;
    [SerializeField] float inTime, outTime;
    Tween tween;
    Sequence sequence;

    void Awake()
    {
        if (!group) group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    public void ScreenTaken()
    {
        sequence.Complete();

        sequence = DOTween.Sequence();
        sequence.Append(group.DOFade(1, inTime));
        sequence.Append(group.DOFade(0, outTime));
    }
}
