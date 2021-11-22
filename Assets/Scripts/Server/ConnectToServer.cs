using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using System.Collections;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text header;

    private void Start()
    {
        StartCoroutine(UpdateHeader());
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    private IEnumerator UpdateHeader()
    {
        while (true)
        {
            header.text = "Connecting";
            yield return new WaitForSeconds(0.5f);
            header.text = "Connecting.";
            yield return new WaitForSeconds(0.5f);
            header.text = "Connecting..";
            yield return new WaitForSeconds(0.5f);
            header.text = "Connecting...";
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(UpdateHeader());
        }
    }

    private void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
}
