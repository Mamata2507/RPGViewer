using UnityEngine;
using Photon.Pun;

namespace RPG
{
    public static class Application
    {
        #region variables
        // Application resolution
        private static Resolution fullResolution;

        // Handle windowed mode
        private static bool isWindowed;
        #endregion

        #region Application Closing
        private static void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("PlayerID", PhotonNetwork.LocalPlayer.ActorNumber);
        }
        #endregion

        #region Start & Update
        private static void Start()
        {
            if (UnityEngine.Application.platform == RuntimePlatform.Android) return;

            // Setting full resolution of screen
            fullResolution = Screen.currentResolution;

            // Start in windowed mode
            if (!Screen.fullScreen)
            {
                isWindowed = true;
                Screen.fullScreen = false;
            }

            Screen.SetResolution(fullResolution.width / 2, fullResolution.height / 2, false);
        }

        private static void Update()
        {
            HandeWindow();

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                // Closing application when pressing Alt + Esc
                if (Input.GetKeyDown(KeyCode.Escape)) UnityEngine.Application.Quit();
            }
        }
        #endregion

        #region Handle Window
        /// <summary>
        /// Handling fullscreen and windowed modes
        /// </summary>
        private static void HandeWindow()
        {
            // Toggling fullscreen on & off by pressing F11
            if (Input.GetKeyDown(KeyCode.F11) && UnityEngine.Application.platform != RuntimePlatform.Android)
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
    }
}
