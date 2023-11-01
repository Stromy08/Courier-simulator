using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 2f;
    private float X, Y;
    float cameraZoom;
    [SerializeField] float zoomSpeed = 4.0f; // Adjust the zoom speed as needed

    void Start()
    {
        X = transform.eulerAngles.y;
        Y = transform.eulerAngles.x;
        cameraZoom = -6;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Control camera zoom with the scroll wheel
        cameraZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1;
        // Limit the minimum and maximum zoom levels if needed
        cameraZoom = Mathf.Clamp(cameraZoom, -10f, -2f); // Adjust the min and max values as needed
    }

    void LateUpdate()
    {
        if (target)
        {
            X += Input.GetAxis("Mouse X") * rotationSpeed;
            Y -= Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion rotation = Quaternion.Euler(Y, X, 0);
            transform.rotation = rotation;
            transform.position = rotation * new Vector3(0, 0, cameraZoom) + target.position;
        }
    }
}
