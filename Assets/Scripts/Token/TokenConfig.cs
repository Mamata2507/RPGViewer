using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using FunkyCode;
using ExitGames.Client.Photon;
using TMPro;

namespace RPG
{
    public class TokenConfig : MonoBehaviourPun
    {
        #region Variables        
        [SerializeField] private GameObject configPanel;
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text header, elevation;
        [SerializeField] private TMP_InputField elevationInput;
        [SerializeField] private TMP_InputField health;

        public Light2D rLight, fLight;

        public object[] data;
        private const byte HIDE_TOKEN = 1;

        public GameObject config, selectionPanel;
        #endregion

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += HideToken;
        }

        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= HideToken;
        }

        private void HideToken(EventData obj)
        {
            if (obj.Code == HIDE_TOKEN)
            {
                object[] datas = (object[])obj.CustomData;

                if (photonView.ViewID == (int)datas[0] && !MasterClient.isMaster) StartCoroutine(Hide((bool)datas[1]));
            }
        }

        private IEnumerator Hide(bool value)
        {
            Debug.Log(value);

            if (!value)
            {
                rLight.gameObject.SetActive(value);
                fLight.gameObject.SetActive(value);
            }

            else
            {
                yield return new WaitForSeconds(0.5f);
                
                rLight.gameObject.SetActive(value);
                fLight.gameObject.SetActive(value);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            if (MasterClient.isMaster) MasterClient.tokens.Add(gameObject);
            if (!MasterClient.isMaster && (int)GetComponent<TokenConfig>().data[4] == 1) fLight.gameObject.SetActive(false);

            transform.localScale = new Vector2(GridData.ScaleToGrid(float.Parse((string)data[2])) / 100f, GridData.ScaleToGrid(float.Parse((string)data[3])) / 100f);
        }

        #region Token Data
        public void LoadData(object[] data)
        {
            photonView.RPC("UpdateData", RpcTarget.AllBuffered, data, photonView.ViewID);
        }

        [PunRPC]
        private void UpdateData(object[] data, int viewID)
        {
            if (photonView.ViewID != viewID) return;
            
            this.data = data;

            header.text = (string)data[1];
            elevationInput.text = (string)data[15];
            elevation.text = (string)data[15];
            
            if ((string)data[15] != "") elevation.text = (string)data[15] + " ft";
            else elevation.text = "";

            health.text = (string)data[16];

            Debug.Log((int)data[4]);

            if (!MasterClient.isMaster && (int)data[4] == 1) fLight.gameObject.SetActive(false);
            else if (!MasterClient.isMaster && (int)data[4] == 0) fLight.gameObject.SetActive(true);

            transform.localScale = new Vector2(GridData.ScaleToGrid(float.Parse((string)data[2])) / 100f, GridData.ScaleToGrid(float.Parse((string)data[3])) / 100f);
            rLight.color = new Color((float)data[10], (float)data[11], (float)data[12]);

            if ((string)data[9] != "")
            {
                rLight.size = float.Parse((string)data[9]) / 5f * GridData.cellSize;
                rLight.coreSize = float.Parse((string)data[9]) / 5f * GridData.cellSize;
            }
            else
            {
                rLight.size = 0f;
                rLight.coreSize = 0f;
            }

            if ((int)data[13] == 1) rLight.GetComponent<LightFlicker>().enabled = true;
            else rLight.GetComponent<LightFlicker>().enabled = false;

            StartCoroutine(DownloadImage());
        }

        public void DeleteToken()
        {
            PhotonNetwork.Destroy(gameObject);
        }

        private IEnumerator DownloadImage()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://storage.googleapis.com/rpgviewer/" + (string)data[0]);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) Debug.LogError(www.error);
            else
            {                
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
                image.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f), (loadedTexture.width + loadedTexture.height) / 2f);
                    
                byte[] textureBytes = image.sprite.texture.EncodeToPNG();
                File.WriteAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Images" + Path.AltDirectorySeparatorChar + (string)data[0], textureBytes);

                image.color = new Color(255, 255, 255, 100);

                if (SceneManager.GetActiveScene().buildIndex == 3) gameObject.SetActive(false);
            }
        }

        public void SaveConfiguration()
        {
            if ((string)data[14] == "") for (int i = 0; i < 10; i++) data[14] += Random.Range(0, 10).ToString();

            data[17] = transform.position.x;
            data[18] = transform.position.y;

            SaveData(data);
        }

        private void SaveData(object[] data)
        {
            if (Directory.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + FindObjectOfType<CurrentScene>().data[13]))
            {
                string savePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + FindObjectOfType<CurrentScene>().data[13] + Path.AltDirectorySeparatorChar + (string)data[14];
                string json = JsonUtility.ToJson(new TokenData(data));

                using StreamWriter writer = new StreamWriter(savePath);

                writer.Write(json);
                writer.Close();
            }
        }
        #endregion

        #region Buttons
        public void OpenSelection()
        {
            if (selectionPanel.activeInHierarchy) return;

            selectionPanel.SetActive(true);
        }

        public void OpenConfig()
        {
            if (config != null) return;
            config = Instantiate(configPanel, new Vector2(960, 540), Quaternion.identity, GameObject.FindGameObjectWithTag("Main Canvas").transform);
            config.GetComponent<TokenConfigPanel>().LoadData(data, gameObject);
        }

        public void ChangeValues()
        {
            data[15] = elevationInput.text;

            if ((string)data[15] != "") elevation.text = (string)data[15] + " ft";
            else elevation.text = "";

            data[16] = health.text;

            SaveData(data);
        }
        #endregion
    }
}