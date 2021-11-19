using TMPro;
using Photon.Pun;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        if (createInput.text != "") PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        if (joinInput.text != "") PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
