using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SFB;
using UnityEngine.EventSystems;
using Photon.Pun;

namespace RPG
{
    public class TokenConfigPanel : MonoBehaviourPun, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Variables
        [Serializable]
        public struct Identity
        {
            public GameObject identityPanel;
            public Button identityButton;

            [Space(10)]
            public Button imageButton;
            public TMP_InputField imagePath;
            public TMP_InputField tokenName;

            [Space(10)]
            public TMP_InputField widthField;
            public TMP_InputField heightField;

            [Space(10)]
            public TMP_Dropdown disposition;
        }

        [Serializable]
        public struct Vision
        {
            public GameObject visionPanel;
            public Button visionButton;

            [Space(10)]
            public Toggle toggle;
            public TMP_InputField radiusField;
            public TMP_Dropdown type;
        }

        [Serializable]
        public struct Light
        {
            public GameObject lightPanel;
            public Button lightButton;

            [Space(10)]
            public Toggle toggle;
            public TMP_InputField radiusInput;

            [Space(10)]
            public Button colorButton;
            public Image color;

            [Space(10)]
            public GameObject colorPicker;
            public FlexibleColorPicker flexibleColor;

            [Space(10)]
            public TMP_Dropdown type;
        }

        [Space(10)]
        [SerializeField] private string cmdStart;
        [SerializeField] private string cmdEnd;

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color normalColor;

        [SerializeField] private TMP_Text header;
        [SerializeField] private GameObject delete;

        [Space(10)]
        public Identity identity;
        public Vision vision;
        public new Light light;

        private GameObject reference;
        private string fileName = "";
        
        private string elevation, health;

        private bool canDrag;
        private Vector3 distance;
        #endregion

        #region Start & Update
        private void Start()
        {
            if (!MasterClient.isMaster) delete.SetActive(false);

            OpenIdentity();

            identity.identityButton.GetComponentInChildren<TMP_Text>().color = selectedColor;
            vision.visionButton.GetComponentInChildren<TMP_Text>().color = normalColor;
            light.lightButton.GetComponentInChildren<TMP_Text>().color = normalColor;
        }

        private void Update()
        {
            light.color.color = light.flexibleColor.color;
        }
        #endregion

        #region Dragging
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(header.GetComponentInParent<RectTransform>(), Input.mousePosition))
            {
                distance = Input.mousePosition - transform.position;
                canDrag = true;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (canDrag && eventData.pointerId == -1) DragPanel();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canDrag = false;
        }

        private void DragPanel()
        {
            Vector3 mousePos = Input.mousePosition;

            transform.position = new Vector3(mousePos.x - distance.x, mousePos.y - distance.y);
        }
        #endregion

        #region Buttons
        public void OpenIdentity()
        {
            if (MasterClient.isMaster)
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 230f);

                RectTransform[] rects = identity.disposition.GetComponentsInParent<RectTransform>();
                foreach (var item in rects) if (item.gameObject.name == "Disposition") item.gameObject.SetActive(true);
            }
            else
            {
                GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 195f);

                RectTransform[] rects = identity.disposition.GetComponentsInParent<RectTransform>();
                foreach (var item in rects) if (item.gameObject.name == "Disposition") item.gameObject.SetActive(false);
            }
            
            if (vision.visionPanel.activeInHierarchy) GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y - 35f / 2f);

            identity.identityPanel.SetActive(true);
            vision.visionPanel.SetActive(false);
            light.lightPanel.SetActive(false);

            identity.identityButton.GetComponentInChildren<TMP_Text>().color = selectedColor;
            vision.visionButton.GetComponentInChildren<TMP_Text>().color = normalColor;
            light.lightButton.GetComponentInChildren<TMP_Text>().color = normalColor;
        }

        public void OpenVision()
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 195f);
            if (identity.identityPanel.activeInHierarchy || light.lightPanel.activeInHierarchy) GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y + 35f / 2f);

            identity.identityPanel.SetActive(false);
            vision.visionPanel.SetActive(true);
            light.lightPanel.SetActive(false);

            identity.identityButton.GetComponentInChildren<TMP_Text>().color = normalColor;
            vision.visionButton.GetComponentInChildren<TMP_Text>().color = selectedColor;
            light.lightButton.GetComponentInChildren<TMP_Text>().color = normalColor;
        }

        public void OpenLight()
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, 230f);
            if (vision.visionPanel.activeInHierarchy) GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y - 35f / 2f);

            identity.identityPanel.SetActive(false);
            vision.visionPanel.SetActive(false);
            light.lightPanel.SetActive(true);

            identity.identityButton.GetComponentInChildren<TMP_Text>().color = normalColor;
            vision.visionButton.GetComponentInChildren<TMP_Text>().color = normalColor;
            light.lightButton.GetComponentInChildren<TMP_Text>().color = selectedColor;
        }

        public void CloseConfiguration()
        {
            Destroy(gameObject);
        }

        public void DeleteToken()
        {
            if (reference.GetComponent<TokenConfig>() != null)
            {
                if (Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + FindObjectOfType<CurrentScene>().data[13]))
                {
                    File.Delete(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + FindObjectOfType<CurrentScene>().data[13] + Path.AltDirectorySeparatorChar + (string)reference.GetComponent<TokenConfig>().data[14]);
                }
                reference.GetComponent<TokenConfig>().DeleteToken();
                MasterClient.tokens.Remove(reference);
                Destroy(gameObject);
            }
            else if (reference.GetComponent<TokenSidebar>() != null)
            {
                File.Delete(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Tokens" + Path.AltDirectorySeparatorChar + (string)reference.GetComponent<TokenSidebar>().data[14]);

                Destroy(gameObject);
                Destroy(reference);
            }
        }

        public void OpenColorPicker()
        {
            light.colorPicker.SetActive(true);
        }

        public void CloseColorPicker()
        {
            light.colorPicker.SetActive(false);
        }
        #endregion

        #region File Management
        public void SelectFile()
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Image", "", "", false);
            if (paths.Length == 0) return;

            string[] f = paths[0].Split(new string[] { @"\" }, StringSplitOptions.None);
            foreach (var file in f)
            {
                if (file.Contains("."))
                {
                    identity.imagePath.text = file;

                    string sourceFile = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + file;
                    File.Copy(paths[0], sourceFile, true);

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = cmdStart + paths[0] + cmdEnd;
                    process.Start();
                }
            }
        }
        #endregion

        #region Save & Load Data
        public void SaveConfiguration()
        {
            if (identity.imagePath.text == "" || identity.widthField.text == "" || identity.heightField.text == "") return;

            if (fileName == "")
            {
                for (int i = 0; i < 10; i++) fileName += UnityEngine.Random.Range(0, 10).ToString();
            }

            object[] configuration =
            {
                identity.imagePath.text, identity.tokenName.text, identity.widthField.text, identity.heightField.text,
                identity.disposition.value, vision.toggle.isOn,
                vision.radiusField.text, vision.type.value, light.toggle.isOn, light.radiusInput.text,
                light.color.color.r, light.color.color.g, light.color.color.b, light.type.value, fileName, elevation, health, 0.0f, 0.0f
            };
            if (reference.GetComponent<TokenSidebar>() != null)
            {
                SaveData(configuration);
                reference.GetComponent<TokenSidebar>().RefreshToken(configuration);
                Destroy(gameObject);
            }
            else if (reference.GetComponent<TokenSelection>())
            {
                SaveData(configuration);
                FindObjectOfType<SidebarConfig>().SetTokens(configuration);
                Destroy(gameObject);
            }
            else if (reference.GetComponent<TokenConfig>() != null)
            {
                reference.GetComponent<TokenConfig>().LoadData(configuration);
                Destroy(gameObject);
            }
                
        }

        private void SaveData(object[] data)
        {
            if (!Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Tokens")) Directory.CreateDirectory(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Tokens");

            string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Tokens" + Path.AltDirectorySeparatorChar + fileName;
            string json = JsonUtility.ToJson(new TokenData(data));

            using StreamWriter writer = new StreamWriter(savePath);
            
            writer.Write(json);
            writer.Close();
        }

        public void LoadData(object[] data, GameObject reference)
        {
            if (data != null)
            {
                identity.imagePath.text = (string)data[0];
                identity.tokenName.text = (string)data[1];

                identity.widthField.text = (string)data[2];
                identity.heightField.text = (string)data[3];

                identity.disposition.value = (int)data[4];

                vision.toggle.isOn = (bool)data[5];
                vision.radiusField.text = (string)data[6];
                vision.type.value = (int)data[7];

                light.toggle.isOn = (bool)data[8];
                light.radiusInput.text = (string)data[9];

                light.flexibleColor.color = new Color((float)data[10], (float)data[11], (float)data[12]);
                light.type.value = (int)data[13];
                fileName = (string)data[14];
                
                elevation = (string)data[15];
                health = (string)data[16];
            }
            if (reference != null)
            {
                this.reference = reference;

                if (reference.GetComponent<TokenSidebar>() != null) header.text = "Blueprint Configuration: " + (string)data[1];
                else if (reference.GetComponent<TokenConfig>() != null) header.text = "Token Configuration: " + (string)data[1];
                else if (reference.GetComponent<TokenSelection>() != null)
                {
                    header.text = "Create New Token";
                    delete.SetActive(false);
                }
            }
        }
        #endregion
    }
}
