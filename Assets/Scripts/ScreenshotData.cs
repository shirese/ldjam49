using UnityEngine;

[System.Serializable]
public class ScreenshotData
{
    public Texture2D tex;
    public float score;
    public PhotoTargetInfo[] contains;

    public void UpdateScore()
    {
        float total = 0;

        for (int i = 0; i < contains.Length; i++)
        {
            if(contains[i]) total += contains[i].points;
        }

        score = total;
    }
}
