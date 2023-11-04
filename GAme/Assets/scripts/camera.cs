using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 2f;
    private float X, Y;
    float cameraZoom;
    [SerializeField] float zoomSpeed = 4.0f;
    public DeliveryManager deliveryManager;
    public bool paused = false;

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
        // control camera zoom with scroll wheel
        cameraZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1; 
        cameraZoom = Mathf.Clamp(cameraZoom, -10f, -2f); // min and max zoom values
    }

    void LateUpdate()
    {
        if (target && !paused)
        {
            X += Input.GetAxis("Mouse X") * rotationSpeed;
            Y -= Input.GetAxis("Mouse Y") * rotationSpeed;

            Quaternion rotation = Quaternion.Euler(Y, X, 0);
            transform.rotation = rotation;
            transform.position = rotation * new Vector3(0, 0, cameraZoom) + target.position;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
