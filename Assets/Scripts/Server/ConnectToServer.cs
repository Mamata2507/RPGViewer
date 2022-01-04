using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using System.Collections;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    #region Variables
    // Header text field
    [SerializeField] private TMP_Text header;

    // Check connection state
    private bool connected = false;
    #endregion

    #region Start & Update
    private void Start()
    {
        // Update Header
        StartCoroutine(UpdateHeader());
    }
    #endregion

    #region Connection
    /// <summary>
    /// Connect to the server
    /// </summary>
    private void Connect()
    {
        // Prevent duplicate connections
        connected = true;

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
        while (Assets.maps.Count == 0)
        {
            header.text = "Downloading";
            yield return new WaitForSeconds(0.5f);
            header.text = "Downloading.";
            yield return new WaitForSeconds(0.5f);
            header.text = "Downloading..";
            yield return new WaitForSeconds(0.5f);
            header.text = "Downloading...";
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(UpdateHeader());
        }

        while (Assets.maps.Count >= 1)
        {
            // Start connecting to server
            if (!connected) Connect();

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
