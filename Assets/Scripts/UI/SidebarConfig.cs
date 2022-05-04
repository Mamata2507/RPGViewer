using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using FunkyCode;

namespace RPG
{
    public class SidebarConfig : MonoBehaviourPun
    {
        #region Variables
        [Header("Side Panel")]
        [SerializeField] private GameObject sidePanel;
        [SerializeField] private GameObject topPanel;
        [SerializeField] private GameObject buttons;
        [SerializeField] private GameObject contentPanel;
        [SerializeField] private GameObject tokenPanel;
        [SerializeField] private GameObject scenePanel;
        [SerializeField] private GameObject imagePanel;
        [SerializeField] private GameObject border;


        [Space(10)]
        [Header("Buttons")]
        [SerializeField] private Image tokenButton;
        [SerializeField] private Image sceneButton;
        [SerializeField] private Image imageButton;
        [SerializeField] private Button closeButton;

        [Space(10)]
        [SerializeField] private GameObject tokenPrefab;
        [SerializeField] private GameObject scenePrefab;
        [SerializeField] private GameObject imagePrefab;

        [Space(10)]
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color normalColor;

        public GameObject lightingManager;
        public GameObject currentScene;

        private bool tokensLoaded = false;
        #endregion

        #region Start & Updata
        private void Start()
        {
            lightingManager = GameObject.Find("Lighting Manager");
            DontDestroyOnLoad(lightingManager);

            if (FindObjectOfType<CurrentScene>() != null) currentScene = FindObjectOfType<CurrentScene>().gameObject;
            if (currentScene == null)lightingManager.GetComponent<LightingManager2D>().setProfile.DarknessColor = Color.HSVToRGB(0, 0, 0);

            if (!MasterClient.isMaster)
            {
                sidePanel.SetActive(false);
                return;
            }

            if (MasterClient.tokens.Count > 0)
            {
                foreach (var token in MasterClient.tokens) token.SetActive(true);
            }

            tokenButton.color = normalColor;
            sceneButton.color = normalColor;
            imageButton.color = normalColor;

            topPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
            buttons.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
            buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 0);
            sidePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 1060);

            closeButton.gameObject.SetActive(false);
            border.SetActive(false);

            LoadScenes();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && MasterClient.isMaster)
            {
                LightingManager2D lightingManager2D = lightingManager.GetComponent<LightingManager2D>();
                switch (lightingManager2D.cameras.Get(0).Lightmaps[0].rendering)
                {
                    case CameraLightmap.Rendering.Enabled:
                    {
                        CameraSettings settings = new CameraSettings();
                        settings.Lightmaps[0].rendering = CameraLightmap.Rendering.Disabled;

                        lightingManager2D.cameras.cameraSettings[0] = settings;

                        break;
                    }
                    
                    case CameraLightmap.Rendering.Disabled:
                    {
                        CameraSettings settings = new CameraSettings();
                        settings.Lightmaps[0].rendering = CameraLightmap.Rendering.Enabled;

                        lightingManager2D.cameras.cameraSettings[0] = settings;

                        break;
                    }
                }
            }
        }
        #endregion

        #region Buttons
        public void OpenTokens()
        {
            if (!contentPanel.activeInHierarchy) buttons.transform.localPosition = new Vector3(buttons.transform.localPosition.x, buttons.transform.localPosition.y + 2, buttons.transform.localPosition.z);

            contentPanel.SetActive(true);
            closeButton.gameObject.SetActive(true);

            topPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            buttons.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
            buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-80, 0);
            sidePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 1060);
            contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 1035);

            tokenPanel.SetActive(true);
            scenePanel.SetActive(false);
            imagePanel.SetActive(false);
            border.SetActive(true);

            tokenButton.color = selectedColor;
            sceneButton.color = normalColor;
            imageButton.color = normalColor;

            if (!tokensLoaded)
            {
                LoadTokens();
                tokensLoaded = true;
            }
        }

        public void OpenScenes()
        {
            if (!contentPanel.activeInHierarchy) buttons.transform.localPosition = new Vector3(buttons.transform.localPosition.x, buttons.transform.localPosition.y + 2, buttons.transform.localPosition.z);

            contentPanel.SetActive(true);
            closeButton.gameObject.SetActive(true);

            topPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 30);
            buttons.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 30);
            buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-151, 0);
            sidePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 1060);
            contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 1035);

            tokenPanel.SetActive(false);
            scenePanel.SetActive(true);
            imagePanel.SetActive(false);
            border.SetActive(true);

            tokenButton.color = normalColor;
            sceneButton.color = selectedColor;
            imageButton.color = normalColor;
        }

        public void OpenImages()
        {
            if (!contentPanel.activeInHierarchy) buttons.transform.localPosition = new Vector3(buttons.transform.localPosition.x, buttons.transform.localPosition.y + 2, buttons.transform.localPosition.z);

            contentPanel.SetActive(true);
            closeButton.gameObject.SetActive(true);

            topPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 30);
            buttons.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 30);
            buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-151, 0);
            sidePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 1060);
            contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(302, 1035);

            tokenPanel.SetActive(false);
            scenePanel.SetActive(false);
            imagePanel.SetActive(true);
            border.SetActive(true);

            tokenButton.color = normalColor;
            sceneButton.color = normalColor;
            imageButton.color = selectedColor;
        }

        public void CloseConfg()
        {
            buttons.transform.localPosition = new Vector3(buttons.transform.localPosition.x, buttons.transform.localPosition.y - 2, buttons.transform.localPosition.z);

            contentPanel.SetActive(false);
            closeButton.gameObject.SetActive(false);
            border.SetActive(false);

            topPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
            buttons.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 30);
            buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, 0);
            sidePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 1060);
            sidePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 1060);

            tokenButton.color = normalColor;
            sceneButton.color = normalColor;
            imageButton.color = normalColor;
        }
        #endregion

        #region Tokens
        private void LoadTokens()
        {
            foreach (var file in Directory.GetFiles(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Tokens"))
            {
                using StreamReader reader = new StreamReader(file);
                string json = reader.ReadToEnd();

                TokenData data = JsonUtility.FromJson<TokenData>(json);
                SetTokens(TokenToConfig(data));            
            }
        }

        public void SetTokens(object[] data)
        {
            GameObject token = Instantiate(tokenPrefab, tokenPanel.GetComponentInChildren<GridLayoutGroup>().gameObject.transform);
            token.GetComponent<TokenSidebar>().RefreshToken(data);
        }

        private object[] TokenToConfig(TokenData data)
        {
            object[] configuration =
            {
                data.path, data.name,
                data.width, data.height, data.disposition,
                data.vision, data.visionRadius, data.visionType,
                data.light, data.lightRadius,
                data.lightR, data.lightG, data.lightB,
                data.lightType, data.fileName, data.elevation, data.health,
                data.xPos, data.yPos
            };

            return configuration;
        }
        #endregion

        #region Scenes
        private void LoadScenes()
        {
            foreach (var directory in Directory.GetDirectories(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes"))
            {
                foreach (var file in Directory.GetFiles(directory))
                {
                    if (file == directory + @"\Configuration")
                    {
                        using StreamReader reader = new StreamReader(file);
                        string json = reader.ReadToEnd();

                        SceneData data = JsonUtility.FromJson<SceneData>(json);

                        SetScenes(SceneToConfig(data));
                    }
                } 
            }
        }

        public void SetScenes(object[] data)
        {
            GameObject scene = Instantiate(scenePrefab, scenePanel.GetComponentInChildren<GridLayoutGroup>().gameObject.transform);
            scene.GetComponent<SceneSidebar>().RefreshScene(data);
        }

        private object[] SceneToConfig(SceneData data)
        {
            object[] configuration =
            {
                data.path, data.name, data.snapToGrid,
                data.columns, data.rows, data.area, data.position, data.startPos,
                data.gridR, data.gridG, data.gridB, data.opacity,
                data.vision, data.fileName
            };

            return configuration;
        }
        #endregion
    }
}
