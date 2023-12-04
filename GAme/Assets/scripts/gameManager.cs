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
    public InstantiatePlayerScript instantiatePlayerScript;
    public GameObject CarUI;

    // Start is called before the first frame update
    void Start()
    {   
        CarUI.SetActive(false);
        instantiatePlayerScript.InstantiatePlayer();
        IsDriving = false;
        DefinePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (IsDriving)
            {
                CarUI.SetActive(false);
                IsDriving = false;
                instantiatePlayerScript.InstantiatePlayer();
                DefinePlayer();
            }
            else
            {
                if (PlayerController.IsInCarHitbox)
                {
                    IsDriving = true;
                    Destroy(playerPrefab);
                    DefinePlayer();
                    CarUI.SetActive(true);
                }
            }
        }
    }

    public void DefinePlayer()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        PlayerController = playerPrefab.GetComponent<PlayerController>();
        CameraController.DefinePlayer();
    }
}