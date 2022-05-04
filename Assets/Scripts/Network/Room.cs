using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections;

namespace RPG
{
    public class Room : MonoBehaviourPunCallbacks
    {

        public void CreateRoom()
        {
            if (!PhotonNetwork.InRoom) StartCoroutine(CreateRoomCoroutine());
        }

        public void JoinRoom()
        {
            if (!PhotonNetwork.InRoom) StartCoroutine(JoinRoomCoroutine());
        }

        private IEnumerator CreateRoomCoroutine()
        {
            string name = "";

            RoomOptions options = new RoomOptions();
            options.CleanupCacheOnLeave = false;
            options.PlayerTtl = -1;

            if (PlayerPrefs.HasKey("Room Name"))
            {
                name = PlayerPrefs.GetString("Room Name");
                MasterClient.isMaster = true;
            }
            else
            {
                name = MasterClient.RoomName();
                PlayerPrefs.SetString("Room Name", name);
                MasterClient.isMaster = true;
            }
            yield return PhotonNetwork.CreateRoom(name, options);

            StopAllCoroutines();
        }

        private IEnumerator JoinRoomCoroutine()
        {
            yield return PhotonNetwork.JoinRandomRoom();

            StopAllCoroutines();
        }

        public override void OnJoinedRoom()
        {
            if (PlayerPrefs.GetString("Room Name") == PhotonNetwork.CurrentRoom.Name)
            {
                MasterClient.isMaster = true;
            }
            else MasterClient.isMaster = false;

            PhotonNetwork.LoadLevel(2);
        }
    }
}
