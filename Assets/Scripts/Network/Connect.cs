using UnityEngine.SceneManagement;
using Photon.Pun;

namespace RPG
{
    public class Connect : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            
            ConnectClient();
        }

        private void ConnectClient()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene(1);
        }
    }
}
