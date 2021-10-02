using System.Collections;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public Camera cam;
    public Canvas _canvas;
    public KeyCode captureInput;
    bool takeScreen;

    public void Update()
    {
        if (Input.GetKeyDown(captureInput))
        {
            takeScreen = true;
            StartCoroutine(CaptureScreen( ScreenShotName() ));
        }
    }

    public void LateUpdate()
    {
        if (takeScreen)
        {
            SaveImage(ScreenShotName());
            takeScreen = false;
        }
    }

    public static string ScreenShotName()
    {
        string folder = Path.Combine(Application.persistentDataPath,"screenshots");
        if(!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        return string.Format("{0}/orbitalShot_{1}.png",
            folder,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public IEnumerator CaptureScreen(string fileName)
    {
        yield return null;
        _canvas.enabled = false;

        yield return new WaitForEndOfFrame();

        SaveImage(fileName);

        _canvas.enabled = true;
    }

    public void SaveImage(string fileName)
    {

        ScreenCapture.CaptureScreenshot(fileName);
        Debug.Log(string.Format("Took screenshot to: {0} ", fileName));

    }
}
