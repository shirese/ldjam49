using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class animBoot : MonoBehaviour
{
    public GameEvent loadPlanetScene;
    Coroutine anim;
    public bool tween;
    public TextMeshProUGUI main, endInput;

    void Start()
    {
        anim = StartCoroutine(Play());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tween)
            {
                End();
            }
            else
            {
                // LOAD LEVEL
                loadPlanetScene.Raise();
            }
        }
    }

    IEnumerator Play()
    {
        tween = true;

        endInput.maxVisibleCharacters = 0;
        main.maxVisibleCharacters = 0;

        for (int i = 0; i < 150; i++)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            main.maxVisibleCharacters++;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        for (int i = 0; i < 300; i++)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            main.maxVisibleCharacters++;
        }

        yield return new WaitForSecondsRealtime(0.5f);

    }

    public void End()
    {
        tween = false;
        StopCoroutine(anim);
        main.maxVisibleCharacters = 9999;
        endInput.maxVisibleCharacters = 9999;
    }
}
