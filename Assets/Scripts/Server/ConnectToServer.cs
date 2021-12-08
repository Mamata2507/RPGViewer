using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using System.Collections;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    #region Variables
    // Header (Connecting...)
    [SerializeField] private TMP_Text header;
    #endregion

    #region Start & Update
    private void Start()
    {
        // Update Header
        StartCoroutine(UpdateHeader());

        // Start connecting to server
        Connect();
    }
    #endregion

    #region Connection
    /// <summary>
    /// Connect to the server
    /// </summary>
    private void Connect()
    {
        // Connect using photon settings
        PhotonNetwork.ConnectUsingSettings();
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
    #endregion

    #region Header
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
    #endregion
}
