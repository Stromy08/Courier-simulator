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

    // Start is called before the first frame update
    void Start()
    {
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
