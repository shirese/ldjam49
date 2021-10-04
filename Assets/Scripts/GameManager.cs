using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    public ui_Target uiTarget;
    public UI_Gallery uiGallery;
    public Canvas menuCanvas;
    public Canvas credits;

    public bool InMenu
    {
        get
        {
            return menuCanvas.enabled;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }

    public void ShowMenu(bool forceOpen = false)
    {
        uiGallery.ShowHide(forceOpen);
        if (uiGallery._canvasGallery.enabled) ShowCredits(false);
    }

    public void ShowCredits(bool state)
    {
        if (uiGallery._canvasGallery.enabled) uiGallery.ShowHide();
        credits.enabled = state;
    }

    public static void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
         //Application.OpenURL(webplayerQuitURL);
        #else
         Application.Quit();
        #endif
    }
}
