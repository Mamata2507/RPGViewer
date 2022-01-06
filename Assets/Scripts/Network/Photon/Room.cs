using TMPro;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections;

namespace RPG
{
    public class Room : MonoBehaviourPunCallbacks
    {
        public TMP_InputField nickname;

        private void Start()
        {
            if (PlayerPrefs.HasKey("Nickname")) nickname.text = PlayerPrefs.GetString("Nickname");
        }

        public void CreateRoom()
        {
            StartCoroutine(CreateRoomCoroutine());
        }

        public void JoinRoom()
        {
            StartCoroutine(JoinRoomCoroutine());
        }

        /// <summary>
        /// Create roome
        /// </summary>
        private IEnumerator CreateRoomCoroutine()
        {
            RoomOptions options = new RoomOptions();
            options.CleanupCacheOnLeave = false;
            string name = RandomName();
            while (true)
            {
                if (nickname.text.ToCharArray().Length == 0) yield return null;
                else yield return PhotonNetwork.CreateRoom(name, options);

                StopAllCoroutines();
            }
        }

        /// <summary>
        /// Join to available room
        /// </summary>
        private IEnumerator JoinRoomCoroutine()
        {
            while (true)
            {
                if (nickname.text.ToCharArray().Length == 0) yield return null;
                else yield return PhotonNetwork.JoinRandomRoom();

                StopAllCoroutines();
            }
        }

        public override void OnJoinedRoom()
        {
            if (nickname.text.ToCharArray().Length != 0)
            {
                PhotonNetwork.NickName = nickname.text;
                PlayerPrefs.SetString("Nickname", nickname.text);
            }

            PhotonNetwork.LoadLevel("Game");
        }

        /// <summary>
        /// Returns a random string with five symbols
        /// </summary>
        private string RandomName()
        {
            string name = "";
            for (int i = 1; i <= 5; ++i)
            {
                bool upperCase = Random.Range(0, 2) == 1;
                int rand = 0;
                if (upperCase) rand = Random.Range(65, 91);
                else rand = Random.Range(97, 123);

                name += (char)rand;
            }

            return name;
        }
    }
}
