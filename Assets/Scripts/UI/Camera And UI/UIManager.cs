using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    // Application resolution
    private Resolution fullResolution;
    private bool isWindowed;

    private void Start()
    {
        isWindowed = true;
        Screen.fullScreen = false;
        fullResolution = Screen.currentResolution;
    }

    private void Update()
    {
        HandeWindow();

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            // Closing application when pressing Alt + Esc
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
    }

    /// <summary>
    /// Handling fullscreen and windowed modes
    /// </summary>
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
}
