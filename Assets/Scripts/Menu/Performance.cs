using UnityEngine;
using TMPro;

namespace RPG
{
    public class Performance : MonoBehaviour
    {
        [SerializeField] private int targetFps;
        [SerializeField] private int shadowQuality;

        [SerializeField] private TMP_InputField fpsInput;
        [SerializeField] private TMP_Dropdown shadowInput;

        private void Start()
        {
            LoadSettings();
            
            // Default quality settings.
            QualitySettings.vSyncCount = 0;
        }

        private void LoadSettings()
        {
            // Load settings from memory
            if (PlayerPrefs.HasKey("TargetFPS")) fpsInput.text = PlayerPrefs.GetInt("TargetFPS").ToString();
            if (PlayerPrefs.HasKey("ShadowQuality")) shadowInput.value = PlayerPrefs.GetInt("ShadowQuality");

            targetFps = int.Parse(fpsInput.text);
            
            if (shadowInput.value == 0) shadowQuality = 0;
            else if (shadowInput.value == 1) shadowQuality = 15;
            else if (shadowInput.value == 2) shadowQuality = 30;
            else if (shadowInput.value == 3) shadowQuality = 45;
            else if (shadowInput.value == 4) shadowQuality = 60;
        }

        public void ChangeSettings()
        {
            targetFps = int.Parse(fpsInput.text);

            if (shadowInput.value == 0) shadowQuality = 0;
            else if (shadowInput.value == 1) shadowQuality = 15;
            else if (shadowInput.value == 2) shadowQuality = 30;
            else if (shadowInput.value == 3) shadowQuality = 45;
            else if (shadowInput.value == 4) shadowQuality = 60;

            PlayerPrefs.SetInt("TargetFPS", targetFps);
            PlayerPrefs.SetInt("ShadowQuality", shadowQuality);
        } 
    }
}