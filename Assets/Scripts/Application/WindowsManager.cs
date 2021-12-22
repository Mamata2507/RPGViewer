using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class WindowsManager : MonoBehaviour
{
    #region variables
    // Application resolution
    private Resolution fullResolution;

    // Handle windowed mode
    private bool isWindowed;

    // Confirmation screen
    [SerializeField] private GameObject confirmationScreen;
    #endregion

    #region Start & Update
    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android) return;
        // Setting full resolution of screen
        fullResolution = Screen.currentResolution;

        // Start in windowed mode
        isWindowed = true;
        Screen.fullScreen = false;
        Screen.SetResolution(fullResolution.width / 2, fullResolution.height / 2, false);
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
    #endregion

    #region Handle Window
    /// <summary>
    /// Handling fullscreen and windowed modes
    /// </summary>
    private void HandeWindow()
    {
        // Toggling fullscreen on & off by pressing F11
        if (Input.GetKeyDown(KeyCode.F11) && Application.platform != RuntimePlatform.Android)
        {
            switch (isWindowed)
            {
                case true:
                {
                    // Setting game to fullscreen
                    isWindowed = false;
                    Screen.fullScreen = true;
                    
                    // Setting game to run on full resolution
                    Screen.SetResolution(fullResolution.width, fullResolution.height, true);
                    break;
                }
                case false:
                {
                    // Setting game to windowed mode
                    isWindowed = true;
                    Screen.fullScreen = false;

                    // Setting game to run on half of the initial resolution
                    Screen.SetResolution(fullResolution.width / 2, fullResolution.height / 2, false);
                    break;
                }
            }
        }
    }
    #endregion

    #region Buttons
    public void ExitApplication()
    {
        Application.Quit();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }

    public void ConfirmLeave()
    {
        confirmationScreen.SetActive(true);
    }

    public void CancelLeave()
    {
        confirmationScreen.SetActive(false);
    }
    #endregion
}
