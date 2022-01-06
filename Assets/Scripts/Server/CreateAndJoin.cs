using TMPro;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections;

namespace RPG
{
    public class CreateAndJoin : MonoBehaviourPunCallbacks
    {
        #region Variables
        // Player name
        public TMP_InputField nickname;
        #endregion

        #region Room Name
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
        #endregion

        #region Start & Update
        private void Start()
        {
            Instantiation.Instantiate();
        }
        #endregion

        #region Buttons
        public void CreateRoom()
        {
            StartCoroutine(CreateRoomCoroutine());
        }

        public void JoinRoom()
        {
            StartCoroutine(JoinRoomCoroutine());
        }
        #endregion

        #region Coroutines
        private IEnumerator CreateRoomCoroutine()
        {
            // Modifying room options
            RoomOptions options = new RoomOptions();
            options.CleanupCacheOnLeave = false;

            // Creating room with random generated name
            string name = RandomName();

            while (true)
            {
                if (nickname.text.ToCharArray().Length == 0) yield return null;
                else yield return PhotonNetwork.CreateRoom(name, options);

                StopAllCoroutines();
            }
        }

        private IEnumerator JoinRoomCoroutine()
        {
            while (true)
            {
                if (nickname.text.ToCharArray().Length == 0) yield return null;
                else yield return PhotonNetwork.JoinRandomRoom();

                StopAllCoroutines();
            }
        }
        #endregion

        #region Connection
        public override void OnJoinedRoom()
        {
            // Setting custom nickname
            if (nickname.text.ToCharArray().Length != 0)
            {
                PhotonNetwork.NickName = nickname.text;
                PlayerPrefs.SetString("Nickname", nickname.text);
            }

            // Loading game scene
            PhotonNetwork.LoadLevel("Game");
        }
        #endregion
    }
}
