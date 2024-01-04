using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
  public bool paused;
  public GameObject pauseUI;
  public AudioClip pauseSound;
  private AudioSource audioSource;

  void Start()
  {
      paused = false;
      audioSource = gameObject.AddComponent<AudioSource>();
      audioSource.clip = pauseSound;
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
          AudioListener.pause = true; // Pause all sounds when the game is paused
          audioSource.Play(); // Play the pause sound
      }
      else
      {
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          Time.timeScale = 1;
          pauseUI.SetActive(false);
          AudioListener.pause = false; // Resume all sounds when the game is unpaused
          audioSource.Stop(); // Stop the pause sound
      }
  }
}