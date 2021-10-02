using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public Vector2 currentRotation;
    public Vector2 mouseMove = Vector2.zero;

    [Header("Main params")]
    [SerializeField] Vector3 amplitude;
    [SerializeField] float sensitivity = 1.5f;

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
        mouseMove.x = Input.GetAxis("Mouse X") * sensitivity;
        mouseMove.y = -Input.GetAxis("Mouse Y") * sensitivity;

        currentRotation += mouseMove;

        currentRotation.x = Mathf.Clamp(currentRotation.x, -amplitude.x, amplitude.x);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -amplitude.y, amplitude.y);

        transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        
    }
}