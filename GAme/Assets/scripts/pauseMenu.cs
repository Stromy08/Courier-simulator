using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject pauseUI;
    public car1 carScript;
    public camera cameraScript;

    void Start()
    {
        paused = false;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = !paused;
        }
        checkPause();
    }

    void checkPause()
    {
        if (paused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            pauseUI.SetActive(true);

            cameraScript.paused = true;
            carScript.paused = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseUI.SetActive(false);

            cameraScript.paused = false;
            carScript.paused = false;
        }
    }

}
