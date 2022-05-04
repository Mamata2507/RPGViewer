using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SFB;
using UnityEngine.EventSystems;

namespace RPG
{
    public class SceneConfigPanel: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Variables
        [Serializable]
        public struct Appearance
        {            
            public Button imageButton;
            public TMP_InputField imagePath;
            public TMP_InputField sceneName;
        }

        [Serializable]
        public struct Grid
        {
            public Toggle snapToGrid;

            public TMP_InputField columns;
            public TMP_InputField rows;

            public Vector2 area;
            public Vector2 position;
            public Vector2 startPos;

            [Space(10)]
            public Button colorButton;
            public Image color;

            [Space(10)]
            public GameObject colorPicker;
            public FlexibleColorPicker flexibleColor;

            [Space(10)]
            public Slider Slider;
            public TMP_InputField opacity;
        }

        [Serializable]
        public struct Vision
        {
            public Toggle toggle;
        }

        [Space(10)]
        public Appearance appearance;
        public Grid grid;
        public Vision vision;
        
        [Space(10)]
        [SerializeField] private string cmdStart;
        [SerializeField] private string cmdEnd;
        [SerializeField] private TMP_Text header;
        
        private string fileName = "";

        private bool canDrag;
        private Vector3 distance;
        #endregion

        #region Start & Update
        private void Update()
        {
            grid.color.color = grid.flexibleColor.color;
            grid.opacity.text = grid.Slider.value.ToString();

            FindObjectOfType<GridManager>().ChangeColor(grid.flexibleColor.color, float.Parse(grid.opacity.text) / 100);
        }
        #endregion

        #region Buttons
        public void OpenColorPicker()
        {
            grid.colorPicker.SetActive(true);
        }

        public void CloseColorPicker()
        {
            grid.colorPicker.SetActive(false);
        }

        public void CloseConfiguration()
        {
            SaveConfiguration();
        }

        public void ChangeOpacity()
        {
            grid.Slider.value = float.Parse(grid.opacity.text);
        }

        public void ChangeGrid()
        {
            int resultX = 42;
            int resultY = 29;
            if (int.TryParse(grid.columns.text, out resultX) == false || int.TryParse(grid.rows.text, out resultY) == false) return;

            FindObjectOfType<GridManager>().rows = resultY;
            FindObjectOfType<GridManager>().columns = resultX;
            
            FindObjectOfType<GridManager>().InstantiateCells();
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
                    string sourceFile = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + file;
                    File.Copy(paths[0], sourceFile, true);

                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + file));
                    GameObject.Find("Scene").GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);

                    FindObjectOfType<GridArea>().SetCorners(GameObject.Find("Scene").GetComponent<SpriteRenderer>().sprite);

                    appearance.imagePath.text = file;

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
            if (appearance.imagePath.text == "" || grid.columns.text == "" || grid.rows.text == "") return;

            if (fileName == "")
            {
                for (int i = 0; i < 10; i++) fileName += UnityEngine.Random.Range(0, 10).ToString();
            }
            
            grid.area = new Vector2(Math.Abs(GameObject.Find("Top Left").transform.position.x - GameObject.Find("Top Right").transform.position.x), Math.Abs(GameObject.Find("Top Left").transform.position.y - GameObject.Find("Bot Left").transform.position.y));
            grid.position = FindObjectOfType<GridManager>().transform.position;
            
            object[] configuration =
            {
                appearance.imagePath.text, appearance.sceneName.text,
                grid.snapToGrid.isOn, grid.columns.text, grid.rows.text,
                grid.area, grid.position, grid.startPos,
                grid.color.color.r, grid.color.color.g, grid.color.color.b,
                grid.opacity.text, vision.toggle.isOn, fileName

            };

            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)configuration[0]));

            GameObject.Find("Scene").GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);

            FindObjectOfType<SceneConfigSidebar>().data = configuration;

            Destroy(gameObject);
        }

        public void LoadData(object[] data)
        {
            if (data != null)
            {
                appearance.imagePath.text = (string)data[0];
                appearance.sceneName.text = (string)data[1];
                grid.snapToGrid.isOn = (bool)data[2];
                grid.columns.text = (string)data[3];
                grid.rows.text = (string)data[4];
                grid.area = (Vector2)data[5];
                grid.position = (Vector2)data[6];
                grid.startPos = (Vector2)data[7];
                grid.flexibleColor.color = new Color((float)data[8], (float)data[9], (float)data[10]);
                grid.opacity.text = (string)data[11];
                vision.toggle.isOn = (bool)data[12];
                fileName = (string)data[13];

                header.text = "Scene Configuration: " + (string)data[1];
            }
            else header.text = "Create New Scene";
        }
        #endregion
    }
}