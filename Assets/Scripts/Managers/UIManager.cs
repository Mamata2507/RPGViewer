using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Resolution fullResolution;
    private bool isWindowed = true;

    private void Start()
    {
        Screen.fullScreen = false;
        fullResolution = Screen.currentResolution;

        if (Screen.fullScreen == true) isWindowed = false;
        else isWindowed = true;
    }

    private void Update()
    {
        HandeWindow();

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
    }

    private void HandeWindow()
    {
        // Toggling fullscreen on & off by pressing F11
        if (Input.GetKeyDown(KeyCode.F11))
        {
            switch (isWindowed)
            {
                case true:
                {
                    // Setting game to fullscreen, and restoring to full resolution
                    isWindowed = false;
                    Screen.fullScreen = true;
                    Screen.SetResolution(fullResolution.width, fullResolution.height, true);
                    break;
                }
                case false:
                {
                    // Setting game to windowed mode, half of the resolution of fullscreen
                    isWindowed = true;
                    Screen.fullScreen = false;
                    Screen.SetResolution(fullResolution.width / 2, fullResolution.height / 2, false);
                    break;
                }
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
