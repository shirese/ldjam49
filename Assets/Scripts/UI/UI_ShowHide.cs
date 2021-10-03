using UnityEngine;
using TMPro;

public class UI_ShowHide : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [SerializeField] TextMeshProUGUI tmp;

    [Header("Text")]
    [SerializeField] string textIfSeen;
    [SerializeField] string textIfHidden;

    public void ChangeState()
    {
        bool newState = !_gameObject.activeInHierarchy;

        _gameObject.SetActive(newState);
        tmp.text = newState ? textIfSeen : textIfHidden;
    }
}
