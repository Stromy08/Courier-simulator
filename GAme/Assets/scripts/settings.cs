using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings : MonoBehaviour
{
    public GameObject Settings;
    public bool isActive;
    public GameObject OpenSettingsButton;
    public GameObject CloseSettingsButton;

    public GameObject GraphicsMenu;
    public GameObject SoundMenu;

    // Start is called before the first frame update
    void Start()
    {
        Settings.SetActive(false);
        isActive = false;
        pressEscape();
    }

    // Update is called once per frame

    public void pressEscape()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isActive)
            {
                close();
            }
        }
    }
    public void close()
    {
        Settings.SetActive(false);
        OpenSettingsButton.SetActive(true);
        CloseSettingsButton.SetActive(false);
        isActive = false;
    }

    public void open()
    {
        Settings.SetActive(true);
        OpenSettingsButton.SetActive(false);
        CloseSettingsButton.SetActive(true);
        isActive = true;
    }

    public void openGraphicsMenu()
    {
        GraphicsMenu.SetActive(true);
        SoundMenu.SetActive(false);
    }

    public void openSoundMenu()
    {
        GraphicsMenu.SetActive(false);
        SoundMenu.SetActive(true);
    }
}
