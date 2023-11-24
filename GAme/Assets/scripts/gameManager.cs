using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public PlayerController PlayerController;
    public CameraController CameraController;
    public SimpleCarController simpleCarController;
    public bool IsDriving;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        simpleCarController.InstantiatePlayer();
        IsDriving = false;
        DefinePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsDriving);
        if (Input.GetKeyDown(KeyCode.F))
        {
            CameraController.DefinePlayer();
            if (IsDriving)
            {
                IsDriving = false;
                simpleCarController.InstantiatePlayer();
                CameraController.DefinePlayer();
                DefinePlayer();
            }
            else
            {
                if (PlayerController.IsInCarHitbox)
                {
                    IsDriving = true;
                    Destroy(playerPrefab);
                    CameraController.DefinePlayer();
                }
            }
        }
    }
    public void DefinePlayer()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        PlayerController = playerPrefab.GetComponent<PlayerController>();
    }
}
