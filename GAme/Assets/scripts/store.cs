using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour
{
    public GameObject carMenu;
    public GameObject playerMenu;
    public GameObject boostsMenu;
    public GameObject miscMenu;
    public GameObject shopUI;
    public pauseMenu pauseScript;

    void Start()
    {
        shopUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            shopUI.SetActive(false);
        }
    }

    public void showCarMenu()
    {
        carMenu.SetActive(true);
        playerMenu.SetActive(false);
        boostsMenu.SetActive(false);
        miscMenu.SetActive(false);
    }

    public void showPlayerMenu()
    {
        carMenu.SetActive(false);
        playerMenu.SetActive(true);
        boostsMenu.SetActive(false);
        miscMenu.SetActive(false);
    }

    public void showBoostsMenu()
    {
        carMenu.SetActive(false);
        playerMenu.SetActive(false);
        boostsMenu.SetActive(true);
        miscMenu.SetActive(false);
    }

    public void showMiscMenu()
    {
        carMenu.SetActive(false);
        playerMenu.SetActive(false);
        boostsMenu.SetActive(false);
        miscMenu.SetActive(true);
    }

    public void openShop()
    {
        shopUI.SetActive(true);
        pauseScript.paused = true;
    }

    public void closeShop()
    {
        pauseScript.paused=false;
        shopUI.SetActive(false);
    }

}