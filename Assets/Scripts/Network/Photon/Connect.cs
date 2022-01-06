using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using System.Collections;

namespace RPG
{
    public class Connect : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text header;
        [SerializeField] private GameObject bar;
        [SerializeField] private Image barFill;

        private string[] dots = new string[4];
        
        private bool connected = false;

        private void Start()
        {
            Initiation();
            StartCoroutine(UpdateHeader());
        }

        private void Update()
        {
            barFill.fillAmount = WebRequest.downloadProgress;
        }

        /// <summary>
        /// Initiating header updates
        /// </summary>
        private void Initiation()
        {
            dots[0] = "";
            dots[1] = ".";
            dots[2] = "..";
            dots[3] = "...";
        }

        /// <summary>
        /// Connect to server
        /// </summary>
        private void ConnectClient()
        {
            connected = true;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene("Lobby");
        }

        /// <summary>
        /// Updating header text
        /// </summary>
        private IEnumerator UpdateHeader()
        {
            while (!connected)
            {
                if (Assets.maps.Count == 0)
                {
                    bar.SetActive(true);
                    for (int i = 0; i < 4; i++) header.text = "Downloading" + dots[i];
                }

                else if (Assets.maps.Count >= 1)
                {
                    bar.SetActive(false);
                    ConnectClient();

                    for (int i = 0; i < 4; i++) header.text = "Connecting" + dots[i];
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
