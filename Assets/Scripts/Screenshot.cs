using System.Collections;
using System.Collections.Generic;
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
    public List<Texture2D> gallery;

    [Header("Display info on UI")]
    public UI_TypeText saveMessage;
    public TextMeshProUGUI messageCount;

    private void Awake()
    {
        // CHECK FOLDER
        string folder = GetFolder();

        #if UNITY_EDITOR || UNITY_STANDALONE
        string[] files = Directory.GetFiles(folder, "*.png", SearchOption.TopDirectoryOnly);
        count = files.Length;
        #endif
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
        // IF BUILD OR EDITOR
        #if UNITY_EDITOR || UNITY_STANDALONE
        ScreenCapture.CaptureScreenshot(fileName);
        #endif
        Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture();
        gallery.Add(tex);
        count++;
        UpdateText(fileName);
    }

    void UpdateText(string fileName)
    {
        string filePath = "VVD://NASA/classified/screenshots/" + Path.GetFileName(fileName);
        string saveMessageText = "OrbitalShot saved to: " + filePath;
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

    public void OpenOsExplorer()
    {
        string folderPath = GetFolder();
        folderPath = folderPath.Replace(@"/", @"\");
        Debug.Log(folderPath);
        System.Diagnostics.Process.Start("explorer.exe", "/root," + folderPath);
        // Application.OpenURL("file://[dir]");
    }
}
