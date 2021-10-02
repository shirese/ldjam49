using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_TypeText : MonoBehaviour
{
    TextMeshProUGUI tmp;

    public string input = "Saved Orbital Shot number 50";
    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = string.Empty;
    }

    public void SetText(string text)
    {
        StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        tmp.maxVisibleCharacters = 0;
        tmp.text = text;

        for (int i = 0; i < tmp.text.Length+1; i++)
        {
            tmp.maxVisibleCharacters = i;
            yield return new WaitForFixedUpdate();
        }
    }
}
