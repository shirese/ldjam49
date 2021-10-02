using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class Screenshot : MonoBehaviour
{
    public Camera cam;
    public Canvas _canvas;
    public Canvas _canvasDisabled;
    public KeyCode captureInput;
    public bool screenWitouthUI;

    [Header("Storage")]
    public int count = 0;

    [Header("Display info on UI")]
    public UI_TypeText saveMessage;
    public TextMeshProUGUI messageCount;

    private void Awake()
    {
        // CHECK FOLDER
        string folder = GetFolder();
        string[] files = Directory.GetFiles(folder, "*.png", SearchOption.TopDirectoryOnly);
        count = files.Length;
        SetMissionText();
    }

    string GetFolder()
    {
        string folder = Path.Combine(Application.persistentDataPath, "screenshots");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        return folder;
    }

    void Update()
    {
        if (Input.GetKeyDown(captureInput))
        {
            StartCoroutine(CaptureScreen());
        }
    }

    public string ScreenShotName()
    {
        string folder = GetFolder();

        return string.Format("{0}/orbitalShot_{1}.png",
            folder,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public IEnumerator CaptureScreen()
    {
        string fileName = ScreenShotName();

        yield return null;
        if(_canvas && screenWitouthUI) _canvas.enabled = false;
        if (_canvasDisabled) _canvasDisabled.enabled = false;

        yield return new WaitForEndOfFrame();
        SaveImage(fileName);

        if (_canvas && screenWitouthUI) _canvas.enabled = true;
        if (_canvasDisabled) _canvasDisabled.enabled = true;
    }

    public void SaveImage(string fileName)
    {

        ScreenCapture.CaptureScreenshot(fileName);
        count++;

        UpdateText(fileName);
    }

    void UpdateText(string fileName)
    {
        string saveMessageText = "OrbitalShot saved to: " + fileName;
        if (saveMessage) saveMessage.SetText(saveMessageText);
        SetMissionText();
    }

    void SetMissionText()
    {
        if (messageCount)
        {
            string missionName = "Mission EX-A77 \nOrbitalShot " + count.ToString("D5");
            messageCount.text = missionName;
        }
    }
}
