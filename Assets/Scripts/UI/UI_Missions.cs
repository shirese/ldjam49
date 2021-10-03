using UnityEngine;

public class UI_Missions : MonoBehaviour
{
    [SerializeField] Screenshot screenshoter;
    [SerializeField] Transform container;

    public UI_Missions_line prefabLine;
    public PhotoTargetInfo[] infos;

    UI_Missions_line[] lines;

    void Start() 
    {
        InitAll();
    }

    void InitAll()
    {
        Clear();
        lines = new UI_Missions_line[infos.Length];

        for (int i = 0; i < infos.Length; i++)
        {
            lines[i] = Instantiate(prefabLine, container);
            lines[i].Init(infos[i]);
        }
    }

    public void ActualizeAll()
    {
        ScreenshotData[] galleryData = screenshoter.galleryData.ToArray();
        for (int i = 0; i < galleryData.Length; i++)
        {
            ScreenshotData data = galleryData[i];

            if (data != null)
            {
                for (int j = 0; j < data.contains.Length; j++)
                {
                    for(int k = 0; k < lines.Length; k++)
                    {
                        if (lines[i].info == data.contains[j]) lines[i].SetState(true);
                    }
                }
            }
        }
    }

    public void Actualize()
    {
        // LAST ONE
        ScreenshotData data = screenshoter.galleryData[screenshoter.galleryData.Count-1];

        if (data != null)
        {
            for (int j = 0; j < data.contains.Length; j++)
            {
                for (int k = 0; k < lines.Length; k++)
                {
                    if (lines[k].info == data.contains[j]) lines[k].SetState(true);
                }
            }
        }
    }

    void Clear()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            Transform t = container.GetChild(i);
            if (t) Destroy(t.gameObject);
        }
    }
}
