using TMPro;
using Photon.Pun;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    #region Variables
    // Input fields to store names
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private TMP_InputField nickname;
    #endregion

    #region Buttons
    public void CreateRoom()
    {
        // Creating room with specific name
        if (createInput.text != "" && nickname.text != "") PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        // Joining room with specific name
        if (joinInput.text != "" && nickname.text != "") PhotonNetwork.JoinRoom(joinInput.text);
    }
    #endregion

    #region Connection
    public override void OnJoinedRoom()
    {
        // Settin custom nickname
        PhotonNetwork.NickName = nickname.text;

        // Loading game scene
        PhotonNetwork.LoadLevel("Game");
    }
    #endregion
}
