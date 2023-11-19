using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public Transform car;
    public Transform player;
    public SimpleCarController simpleCarController;

    void Start()
    {
        // At the start of the game, the camera follows and looks at the car
        freeLookCamera.Follow = car;
        freeLookCamera.LookAt = car;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (simpleCarController.isControlled)
            {
                // If 'F' is pressed and the car is controlled, make the camera follow and look at the player
                freeLookCamera.Follow = player;
                freeLookCamera.LookAt = player;
            }
            else
            {
                // If 'F' is pressed and the car is not controlled, make the camera follow and look at the car
                freeLookCamera.Follow = car;
                freeLookCamera.LookAt = car;
            }
        }
    }
}