using TMPro;
using Photon.Pun;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    // Input fields to store room names
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;

    public void CreateRoom()
    {
        // Creating room with specific name
        if (createInput.text != "") PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        // Joining room with specific name
        if (joinInput.text != "") PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        // Loading game scene
        PhotonNetwork.LoadLevel("Game");
    }
}
