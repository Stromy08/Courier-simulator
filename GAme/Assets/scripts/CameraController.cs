using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public Transform CameraAnchor;
    public Transform playerPrefab;
    public SimpleCarController simpleCarController;
    public gameManager gameManager;

    void Start()
    {
        DefinePlayer();

        // At the start of the game, the camera follows and looks at the car
        freeLookCamera.Follow = playerPrefab;
        freeLookCamera.LookAt = playerPrefab;
    }

    void Update()
    {
        if (gameManager.IsDriving)
        {
            freeLookCamera.Follow = CameraAnchor;
            freeLookCamera.LookAt = CameraAnchor;
        }
        else
        {
            freeLookCamera.Follow = playerPrefab;
            freeLookCamera.LookAt = playerPrefab;
        }
    }

    public void DefinePlayer()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player").transform;
        CameraAnchor = GameObject.FindGameObjectWithTag("cameraAnchor").transform;
    }   
}