using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using System.Collections;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Header (Connecting...)
    [SerializeField] private TMP_Text header;

    private void Start()
    {
        StartCoroutine(UpdateHeader());
        Connect();
    }

    public override void OnConnectedToMaster()
    {
        //Joining the lobby
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // Loading lobby scene
        SceneManager.LoadScene("Lobby");
    }

    /// <summary>
    /// Updating the visual of the header
    /// </summary>
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

    /// <summary>
    /// Connect to the server
    /// </summary>
    private void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
}
