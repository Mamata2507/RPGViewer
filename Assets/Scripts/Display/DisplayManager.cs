using UnityEngine;

namespace RPG
{
    public class DisplayManager : MonoBehaviour
    {
        private void Start()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer) DisplaySettings.Initiate();
        }

        private void Update()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                HandleInput();
                HandleMode();
            }
        }

        /// <Summary>
        /// Handle keyboard inputs
        /// </Summary>
        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                if (Screen.fullScreen) DisplaySettings.ChangeResolution(true, DisplaySettings.windowedResolution.width, DisplaySettings.windowedResolution.height);
                else DisplaySettings.ChangeResolution(Screen.fullScreen, DisplaySettings.fullResolution.width, DisplaySettings.fullResolution.height);
            }
        }

        /// <Summary>
        /// Handle resolution in widnowed mode
        /// </Summary>
        private void HandleMode()
        {
            if (!Screen.fullScreen)
            {
                DisplaySettings.windowedResolution.width = Screen.width;
                DisplaySettings.windowedResolution.height = Screen.height;
            }
        }
    }
}