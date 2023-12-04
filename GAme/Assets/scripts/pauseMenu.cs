using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject pauseUI;
    public settings settings; // Changed 'Settings' to 'settings'

    void Start()
    {
        paused = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            paused = !paused;
            settings.pressEscape();
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
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }

    
}