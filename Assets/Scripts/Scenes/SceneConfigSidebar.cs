using FunkyCode;
using Photon.Pun;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG
{
    public class SceneConfigSidebar : MonoBehaviour
    {
        [SerializeField] private GameObject scene;
        [SerializeField] private GameObject grid;
        [SerializeField] private GameObject configPanel;

        [Space(10)]
        [SerializeField] private GameObject gridButton;

        private GameObject topLeft, topRight, botLeft;

        private GameObject lightingManager;
        private GameObject selection;
        private GameObject currentScene;

        public object[] data;

        private void Start()
        {
            topLeft = GameObject.Find("Top Left");
            topRight = GameObject.Find("Top Right");
            botLeft = GameObject.Find("Bot Left");

            if (GameObject.Find("Scene Prefab(Clone)") != null) currentScene = GameObject.Find("Scene Prefab(Clone)");

            if (GameObject.Find("Lighting Manager") != null)
            {
                lightingManager = GameObject.Find("Lighting Manager");

                lightingManager.SetActive(false);
            }
            
            if (FindObjectOfType<CurrentScene>() != null) FindObjectOfType<CurrentScene>().gameObject.SetActive(false);

            foreach (var token in MasterClient.tokens) token.SetActive(false);

            if (MasterClient.data != null)
            {
                data = MasterClient.data;

                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)data[0]));

                scene.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100); ;
            }
        }

        #region Buttons
        public void OpenSettings()
        {
            if (selection != null) return;

            grid.SetActive(true);
            gridButton.GetComponent<Image>().color = new Color(gridButton.GetComponentInChildren<Image>().color.r, gridButton.GetComponentInChildren<Image>().color.g, gridButton.GetComponentInChildren<Image>().color.b, 1.00f);

            selection = Instantiate(configPanel, GameObject.FindGameObjectWithTag("Main Canvas").transform, false);
            selection.transform.localPosition = new Vector2(0, 0);
            selection.GetComponent<SceneConfigPanel>().LoadData(data);
        }

        public void ShowGrid()
        {
            if (selection != null) return;

            if (grid.activeInHierarchy) gridButton.GetComponent<Image>().color = new Color(gridButton.GetComponentInChildren<Image>().color.r, gridButton.GetComponentInChildren<Image>().color.g, gridButton.GetComponentInChildren<Image>().color.b, 0.25f);
            else gridButton.GetComponent<Image>().color = new Color(gridButton.GetComponentInChildren<Image>().color.r, gridButton.GetComponentInChildren<Image>().color.g, gridButton.GetComponentInChildren<Image>().color.b, 1.00f);

            grid.SetActive(!grid.activeInHierarchy);
        }

        public void SaveConfig()
        {
            if (data != null)
            {
                data[5] = new Vector2(Math.Abs(topLeft.transform.position.x - topRight.transform.position.x), Math.Abs(topLeft.transform.position.y - botLeft.transform.position.y));
                data[6] = (Vector2)grid.transform.position;
                data[7] = (Vector2)topLeft.transform.position;

                MasterClient.data = data;
                SaveData(data);
            }

            if (currentScene != null) currentScene.gameObject.SetActive(true);
            if (lightingManager != null) lightingManager.SetActive(true);

            lightingManager = null;
            data = null;
            currentScene = null;

            PhotonNetwork.LoadLevel(2);
        }
        #endregion

        private void SaveData(object[] data)
        {
            if (data == null) return;

            if (!Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes")) Directory.CreateDirectory(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes");
            if (!Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + data[13])) Directory.CreateDirectory(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + data[13]);

            string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + data[13] + Path.AltDirectorySeparatorChar + "Configuration";
            string json = JsonUtility.ToJson(new SceneData(data));

            using StreamWriter writer = new StreamWriter(savePath);
            
            writer.Write(json);
            writer.Close();
        }
    }
}