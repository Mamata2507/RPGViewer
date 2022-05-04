using Photon.Pun;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using FunkyCode;

namespace RPG
{
    public class CurrentScene : MonoBehaviourPun
    {
        public object[] data;
        private const byte DESTROY_SCENE = 2;

        [SerializeField] private SpriteRenderer image;
        [SerializeField] private GameObject cell;

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += DestroyScene;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= DestroyScene;
        }

        private void DestroyScene(EventData obj)
        {
            if (obj.Code == DESTROY_SCENE)
            {
                object[] datas = (object[])obj.CustomData;

                if (photonView.ViewID == (int)datas[0]) Destroy(gameObject);
            }
        }

        public void LoadScene(object[] data)
        {
            FindObjectOfType<SidebarConfig>().lightingManager.GetComponent<LightingManager2D>().setProfile.DarknessColor = Color.HSVToRGB(0, 0, 0.25f);


            DontDestroyOnLoad(gameObject);
            if (MasterClient.tokens.Count > 0)
            {
                foreach (var token in MasterClient.tokens)
                {
                    token.GetComponent<TokenConfig>().DeleteToken();
                }
                MasterClient.tokens = new List<GameObject>();
            }
            photonView.RPC("UpdateScene", RpcTarget.AllBuffered, data);
            photonView.RPC("InstantiateGrid", RpcTarget.AllBuffered, data);

            foreach (var file in Directory.GetFiles(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + (string)data[13]))
            {
                if (file != Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + (string)data[13] + @"\Configuration")
                {
                    using StreamReader reader = new StreamReader(file);
                    string json = reader.ReadToEnd();

                    TokenData tokendData = JsonUtility.FromJson<TokenData>(json);

                    GameObject token = PhotonNetwork.Instantiate(@"Prefabs/Tokens/Other/Token", Camera.main.ScreenToWorldPoint(new Vector3(tokendData.xPos, tokendData.yPos, -1)), Quaternion.identity);
                    token.transform.position = new Vector3(tokendData.xPos, tokendData.yPos, -1);

                    token.GetComponent<TokenConfig>().LoadData(TokenToConfig(tokendData));
                }
            }
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

        [PunRPC]
        private void UpdateScene(object[] data)
        {
            this.data = data;

            FindObjectOfType<SidebarConfig>().currentScene = gameObject;


            if ((bool)data[12] == false)
            {
                CameraSettings settings = FindObjectOfType<SidebarConfig>().lightingManager.GetComponent<LightingManager2D>().cameras.cameraSettings[0];
                settings.Lightmaps[0].rendering = CameraLightmap.Rendering.Disabled;
                settings.Lightmaps[1].rendering = CameraLightmap.Rendering.Enabled;

                FindObjectOfType<SidebarConfig>().lightingManager.GetComponent<LightingManager2D>().cameras.cameraSettings[0] = settings;
            }
            else
            {
                CameraSettings settings = FindObjectOfType<SidebarConfig>().lightingManager.GetComponent<LightingManager2D>().cameras.cameraSettings[0];
                settings.Lightmaps[0].rendering = CameraLightmap.Rendering.Enabled;
                settings.Lightmaps[1].rendering = CameraLightmap.Rendering.Enabled;

                FindObjectOfType<SidebarConfig>().lightingManager.GetComponent<LightingManager2D>().cameras.cameraSettings[0] = settings;
            }

            if (File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)data[0]))
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)data[0]));
                image.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
            }
            else
            {
                StartCoroutine(DownloadImage());
            }
        }

        [PunRPC]
        private void InstantiateGrid(object[] data)
        {
            GridData.positions.Clear();

            Vector2 area = (Vector2)data[5];
            Vector2 position = (Vector2)data[6];
            Vector2 startPos = (Vector2)data[7];

            float size = area.x / int.Parse((string)data[3]);

            GridData.cellSize = size;

            GameObject.Find("Grid Canvas").transform.localPosition = position;

            for (int x = 0; x < int.Parse((string)data[3]); x++)
            {
                for (int y = 0; y < int.Parse((string)data[4]); y++)
                {
                    GameObject cell = Instantiate(this.cell, GameObject.Find("Grid Canvas").transform);

                    cell.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                    cell.transform.position = new Vector3(startPos.x + (size * x), startPos.y - (size * y), -1);

                    GridData.positions.Add(new Vector2(cell.transform.position.x + (size / 2f), cell.transform.position.y - (size / 2f)));
                    GridData.snapToGrid = (bool)data[2];

                    cell.GetComponent<Image>().color = new Color((float)data[8], (float)data[9], (float)data[10], float.Parse((string)data[11]) / 100);
                }
            }
        }

        private IEnumerator DownloadImage()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://storage.googleapis.com/rpgviewer/" + (string)data[0]);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) Debug.LogError(www.error);
            else
            {
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
                image.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f), 100f);

                byte[] textureBytes = image.sprite.texture.EncodeToPNG();
                File.WriteAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)data[0], textureBytes);
            }
        }
    }
}
