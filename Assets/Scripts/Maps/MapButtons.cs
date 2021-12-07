using UnityEngine;
using Photon.Pun;

public class MapButtons : MonoBehaviourPun
{
    // Currently selected map
    public GameObject map;

    // Accept and Cancel buttons
    public GameObject acceptButton;
    public GameObject cancelButton;

    private void Start()
    {
        acceptButton.SetActive(false);
        cancelButton.SetActive(false);
    }

    /// <summary>
    /// Accepting currently selected map (called when pressing Accept-button)
    /// </summary>
    /// 
    public void AcceptMap()
    {
        MapHandler[] maps = GetComponentsInChildren<MapHandler>();

        foreach (var map in maps)
        {
            if (this.map == map.mapPrefab)
            {
                photonView.RPC("DestroyMap", RpcTarget.All);
                map.AcceptMap();
                this.map = null;
            }

            acceptButton.SetActive(false);
            cancelButton.SetActive(false);
        }
    }

    /// <summary>
    /// Cancelling currently selected map (called when pressing Cancel-button)
    /// </summary>
    public void CancelMap()
    {
        acceptButton.SetActive(false);
        cancelButton.SetActive(false);
        map = null;
    }

    /// <summary>
    /// Destroying current map
    /// </summary>
    [PunRPC]
    private void DestroyMap()
    {
        if (GameObject.FindGameObjectWithTag("Map") != null) Destroy(GameObject.FindGameObjectWithTag("Map"));
    }
}
