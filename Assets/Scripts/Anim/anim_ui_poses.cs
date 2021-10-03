using UnityEngine;
using DG.Tweening;

public class anim_ui_poses : MonoBehaviour
{
    [SerializeField] RectTransform rTransform;
    [SerializeField] Vector2 poses;

    Tween tween;

    void Start()
    {
        MoveToPoseB();
    }

    void MoveToPoseB()
    {
        tween.Complete();
        rTransform.anchoredPosition = new Vector2(0, poses.x);
        tween = rTransform.DOAnchorPos(new Vector2(0, poses.y), 0.5f);
    }
}
