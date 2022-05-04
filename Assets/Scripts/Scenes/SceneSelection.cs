using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG
{
    public class SceneSelection : MonoBehaviourPun
    {
        [SerializeField] private GameObject header;
        [SerializeField] private GameObject confirmPanel;

        private const byte DESTROY_SCENE = 2;
        public object[] data;
        public GameObject reference = null;

        private void Update()
        {
            if (data == null) return;

            if (Input.GetMouseButtonUp(0) && !RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition)) gameObject.SetActive(false);
        }

        #region Buttons
        public void SelectScene()
        {
            if (FindObjectOfType<CurrentScene>() != null)
            {
                RaiseEventOptions options = new RaiseEventOptions();
                options.Receivers = ReceiverGroup.All;
                object[] datas = { FindObjectOfType<CurrentScene>().GetComponent<PhotonView>().ViewID };
                PhotonNetwork.RaiseEvent(DESTROY_SCENE, datas, options, SendOptions.SendReliable);
            }
            GameObject scene = PhotonNetwork.Instantiate(@"Prefabs/Scenes/Other/Scene Prefab", Vector3.zero, Quaternion.identity);
            scene.GetComponent<CurrentScene>().LoadScene(data);

            gameObject.SetActive(false);
        }
        
        public void ConfigScene()
        {
            SceneManager.LoadScene(3);
            PlayerPrefs.SetString("Config", (string)data[13]);

            MasterClient.data = data;

            gameObject.SetActive(false);
        }
        
        public void AddScene()
        {
            MasterClient.data = null;
            SceneManager.LoadScene(3);
        }

        public void DeleteScene()
        {
            GameObject config = Instantiate(confirmPanel, GameObject.FindGameObjectWithTag("Main Canvas").transform, false);
            config.transform.localPosition = new Vector2(0, 0);

            config.GetComponent<SceneSelection>().data = data;
            config.GetComponent<SceneSelection>().reference = reference;

            gameObject.SetActive(false);
        }

        public void CancelDeletion()
        {
            Destroy(gameObject);
        }

        public void ConfirmDeletion()
        {
            foreach (var file in Directory.GetFiles(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + (string)data[13])) File.Delete(file);

            Directory.Delete(Application.persistentDataPath + Path.AltDirectorySeparatorChar + "Scenes" + Path.AltDirectorySeparatorChar + (string)data[13]);

            Destroy(reference);
            Destroy(gameObject);
        }
        #endregion
    }
}