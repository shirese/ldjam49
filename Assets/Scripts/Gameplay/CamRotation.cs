using UnityEngine;

public class CamRotation : MonoBehaviour
{
    GameManager manager;
    public Vector2 currentRotation;
    public Vector2 mouseMove = Vector2.zero;

    [Header("Main params")]
    [SerializeField] Vector3 amplitude;
    [SerializeField] float sensitivity = 1.5f;
    private void Awake()
    {
        manager = GetComponentInParent<GameManager>();
    }

    void Update()
    {
        Rotate();

        if (Input.GetMouseButtonDown(0))
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Rotate()
    {
        if (Time.timeScale <= 0.5f || manager.InMenu) return;

        mouseMove.x = Input.GetAxis("Mouse X") * sensitivity * (1920.0f / Screen.width);
        mouseMove.y = -Input.GetAxis("Mouse Y") * sensitivity * (1920.0f / Screen.width);

        currentRotation += mouseMove;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -amplitude.x, amplitude.x);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -amplitude.y, amplitude.y);

        transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        
    }
}
