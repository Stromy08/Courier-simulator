using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private float X, Y;
    public float carSpeed;
    float cameraZoom;
    [SerializeField] float speedMultiplier;

    void Start()
    {
        X = transform.eulerAngles.y;
        Y = transform.eulerAngles.x;
    }




    void LateUpdate()
    {

        

        if (target)
        {
            if (Input.GetMouseButton(0))
            {
                X += Input.GetAxis("Mouse X") * speed;
                Y -= Input.GetAxis("Mouse Y") * speed;
            }

            Quaternion rotation = Quaternion.Euler(Y, X, 0);
            transform.rotation = rotation;
            transform.position = rotation * new Vector3(0, 0, -6) + target.position;
        }
    }
}