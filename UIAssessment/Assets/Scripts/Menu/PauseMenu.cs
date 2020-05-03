using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject pauseMenu, optionsMenu;
    void Paused()
    {
        isPaused = true;

        Time.timeScale = 0;

        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }

    public void Unpaused()
    {
        isPaused = false;

        Time.timeScale = 1;

        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }
    private void Start()
    {
        Unpaused();
    }
    void TogglePause()
    {
        if(!isPaused)
        {
            Paused();
        }
        else
        {
            Unpaused();
        }
    }
    private void Update()
    {
    if(Input.GetButtonDown("Cancel"))
        {
            //if the options panel is not on
            if(!optionsMenu.activeSelf)
            {
                TogglePause();
            }
            else
            {
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
            }
        }
    }
}
