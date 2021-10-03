using UnityEngine;
using System.Collections;

public class PhotoEvent : MonoBehaviour
{
    public bool happened;
    public float duration;

    [Header("IF TIMED")]
    public float timeToHappen;

    [Header("IF RANDOM")]
    public float chanceToHappen;
    public bool random;

    float chanceByFrame = 0;
    public PhotoTarget toActivate;

    void Awake()
    {
        Activate(false);
        chanceByFrame = chanceToHappen * Time.fixedDeltaTime * 0.25f;
    }

    void FixedUpdate()
    {
        if (happened && !random) return;

        if (random)
        {
            if(Random.value <= chanceByFrame)
            {
                Activate(true);
            }
        }
        else if(Time.timeSinceLevelLoad >= timeToHappen)
        {
            Activate(true);
        }
    }

    void Activate(bool state)
    {
        toActivate.gameObject.SetActive(state);
        if (state)
        {
            happened = true;
            StartCoroutine(Hide(duration));
        }
    }

    IEnumerator Hide(float time)
    {
        yield return new WaitForSeconds(time);
        Activate(false);
    }
}
