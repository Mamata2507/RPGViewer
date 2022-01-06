using UnityEngine;

namespace RPG
{
    public static class DisplaySettings
    {
        [Tooltip("Full display resolution")]
        public static Resolution fullResolution;

        [Tooltip("Windowed display resolution")]
        public static Resolution windowedResolution;

        private static bool initiate = false;

        /// <Summary>
        /// Initiate display settings
        /// </Summary>
        public static void Initiate()
        {
            if (initiate) return;

            fullResolution = Screen.currentResolution;

            windowedResolution.width = Screen.currentResolution.width / 2;
            windowedResolution.height = Screen.currentResolution.height / 2;

            ChangeResolution(true, fullResolution.width / 2, fullResolution.height / 2);
            initiate = true;
        }

        /// <Summary>
        /// Change application resolution
        /// </Summary>
        public static void ChangeResolution(bool windowed, int? width = null, int? height = null)
        {
            Screen.SetResolution(width ?? Screen.currentResolution.width, height ?? Screen.currentResolution.height, !windowed);
        }
    }
}
