using TMPro;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    #region Variables
    // Input fields to store names
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private TMP_InputField nickname;
    #endregion

    #region Room Name
    private string RandomName()
    {
        string name = "";

        for (int i = 1; i <= 4; ++i)
        {
            bool upperCase = Random.Range(0, 2) == 1;

            int rand = 0;
            if (upperCase)
            {
                rand = Random.Range(65, 91);
            }
            else
            {
                rand = Random.Range(97, 123);
            }

            name += (char)rand;
        }
        return name;
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
            if (Assets.maps.Count == 0) yield return null;

            yield return PhotonNetwork.CreateRoom(name, options);
            StopAllCoroutines();
        } 
    }

    private IEnumerator JoinRoomCoroutine()
    {
        while (true)
        {
            if (Assets.maps.Count == 0) yield return null;

            yield return PhotonNetwork.JoinRandomRoom();
            StopAllCoroutines();
        }
    }
    #endregion

    #region Connection
    public override void OnJoinedRoom()
    {
        // Settin custom nickname
        PhotonNetwork.NickName = nickname.text;

        // Loading game scene
        PhotonNetwork.LoadLevel("Game");

        if (PlayerPrefs.GetString("RoomName") != PhotonNetwork.CurrentRoom.Name) PlayerPrefs.SetString("RoomName", PhotonNetwork.CurrentRoom.Name);
    }
    #endregion
}
