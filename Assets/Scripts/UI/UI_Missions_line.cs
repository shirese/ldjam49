using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_Missions_line : MonoBehaviour
{
    public PhotoTargetInfo info;

    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] Sprite empty, done;
    [SerializeField] Color colorEmpty, colorDone;
    [SerializeField] Image img;

    public void Init(PhotoTargetInfo info)
    {
        this.info = info;

        if (tmp && info)
        {
            string targetName = info.publicName;
            bool isVowel = "aeiouAEIOU".IndexOf(targetName[0]) >= 0;
            string article = isVowel ? "an" : "a";
            tmp.text = "Capture " + article + " " + targetName;
        }
        SetState(false);
    }

    public void SetState(bool value)
    {
        img.sprite = value ? done : empty;
        img.color = value ? colorDone : colorEmpty;
    }
}
