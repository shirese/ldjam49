using UnityEngine;

public class hideIfWebGL : MonoBehaviour
{
    public bool showOnlyInWeb;

    void Awake()
    {

#if UNITY_WEBPLAYER

        if(showOnlyInWeb)
        {
            this.gameObject.SetActive(true);
        }
        else  this.gameObject.SetActive(false);

#else

        if (!showOnlyInWeb)
        {
            this.gameObject.SetActive(true);
        }
        else this.gameObject.SetActive(false);

#endif
    }
}
