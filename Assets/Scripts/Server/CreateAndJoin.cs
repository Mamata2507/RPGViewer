using TMPro;
using Photon.Pun;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    // Input fields to store names
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private TMP_InputField nickname;

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

    public override void OnJoinedRoom()
    {
        // Loading game scene
        PhotonNetwork.NickName = nickname.text;
        PhotonNetwork.LoadLevel("Game");
    }
}
