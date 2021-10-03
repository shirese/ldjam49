using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Screenshot : MonoBehaviour
{
    [Header("Components")]
    public Camera cam;
    public Canvas _canvas;
    public Canvas _canvasDisabled;
    public PlayerRaycastView raycaster;

    [Header("Capute")]
    public KeyCode captureInput;
    [SerializeField] GameEvent screenshotTakenEvent;

    [Header("Storage")]
    public int count = 0;
    public List<ScreenshotData> galleryData;

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

        bool prevState = _canvasDisabled.enabled;
        if (_canvas) _canvas.enabled = false;
        if (_canvasDisabled) _canvasDisabled.enabled = false;

        yield return new WaitForEndOfFrame();
        SaveImage(fileName);
        screenshotTakenEvent.Raise();

        if (_canvas) _canvas.enabled = true;
        if (_canvasDisabled && prevState) _canvasDisabled.enabled = true;
    }

    public void SaveImage(string filePath)
    {
        // IF BUILD OR EDITOR
        #if UNITY_EDITOR || UNITY_STANDALONE
        ScreenCapture.CaptureScreenshot(filePath);
#endif

        string fileName = Path.GetFileName(filePath);

        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        tex.Apply();
        // NEW DATA
        ScreenshotData data = new ScreenshotData();
        data.tex = tex;
        data.fileName = fileName;
        data.ID = count;
        data.contains = raycaster.DoBoxCast();
        data.UpdateScore();
        galleryData.Add(data);
        // END
        count++;
        UpdateText(fileName);
    }

    void UpdateText(string fileName)
    {
        string fakeFilePath = "VVD://NASA/classified/screenshots/" + fileName;
        string saveMessageText = "OrbitalShot saved to: " + fakeFilePath;
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
