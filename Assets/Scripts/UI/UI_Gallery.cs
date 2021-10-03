using UnityEngine;

public class UI_Gallery : MonoBehaviour
{
    Canvas _canvasGallery;
    [SerializeField] Screenshot data;

    [Header("Self")]
    [SerializeField] Transform galleryContainer;
    [SerializeField] UI_Screenshot ui_prefab;

    [Header("Big window")]
    [SerializeField] UI_Screenshot bigScreen;

    private void Awake()
    {
        _canvasGallery = GetComponent<Canvas>();
        _canvasGallery.enabled = false;
        bigScreen.gameObject.SetActive(false);
    }

    public void ShowHide(bool forceOpen = false)
    {
        _canvasGallery.enabled = forceOpen || !_canvasGallery.enabled;

        if(_canvasGallery.enabled)
        {
            SpawnAll(data.galleryData.ToArray());
        }
    }

    void SpawnAll(ScreenshotData[] galleryData)
    {
        ClearGallery();

        for (int i = 0; i < galleryData.Length; i++)
        {
            UI_Screenshot ui = Instantiate(ui_prefab, galleryContainer);
            ui.Init(this, galleryData[i]);
        }
    }

    void ClearGallery()
    {
        for (int i = 0; i < galleryContainer.childCount; i++)
        {
            Transform t = galleryContainer.GetChild(i);
            if (t) Destroy(t.gameObject);
        }
    }

    public void ShowBigger(ScreenshotData data)
    {
        bigScreen.Init(this, data);
        bigScreen.gameObject.SetActive(true);
    }
}