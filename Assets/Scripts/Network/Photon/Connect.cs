using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using System.Collections;

namespace RPG
{
    public class Connect : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text header;

        private string[] dots = new string[3];
        
        private bool connected = false;

        private void Start()
        {
            Initiation();
            StartCoroutine(UpdateHeader());
        }

        /// <summary>
        /// Initiating header updates
        /// </summary>
        private void Initiation()
        {
            dots[0] = ".";
            dots[1] = "..";
            dots[2] = "...";
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
            while (Assets.maps.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    header.text = "Downloading" + dots[i];
                    yield return new WaitForSeconds(0.5f);
                }
            }

            while (Assets.maps.Count >= 1)
            {
                if (!connected) ConnectClient();

                for (int i = 0; i < 4; i++)
                {
                    header.text = "Connecting" + dots[i];
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }
}
