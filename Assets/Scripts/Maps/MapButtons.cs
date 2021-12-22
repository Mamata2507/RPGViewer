using UnityEngine;
using Photon.Pun;

public class MapButtons : MonoBehaviourPun
{
    #region Variables
    // Currently selected map
    public GameObject map;

    // Accept and Cancel buttons
    public GameObject acceptButton;
    public GameObject cancelButton;
    #endregion

    #region Start & Update
    private void Start()
    {
        // Hiding buttons
        acceptButton.SetActive(false);
        cancelButton.SetActive(false);
    }
    #endregion

    #region Buttons
    /// <summary>
    /// Accepting currently selected map (called when pressing Accept-button)
    /// </summary>
    /// 
    public void AcceptMap()
    {
        // Getting reference of each map
        MapHandler[] maps = GetComponentsInChildren<MapHandler>();

        foreach (var map in maps)
        {
            if (this.map == map.mapPrefab)
            {
                // Destroying old map
                if (GameObject.FindGameObjectWithTag("Map") != null) 
                {
                    GameObject.FindGameObjectWithTag("Map").GetComponentInChildren<GridManager>().GetComponent<PhotonView>().RPC("DestroyMap", RpcTarget.All);
                }
                
                // Accepting new map
                map.AcceptMap();

                // Clearing reference of selected map
                this.map = null;
            }

            // Hiding buttons
            acceptButton.SetActive(false);
            cancelButton.SetActive(false);
        }
    }

    /// <summary>
    /// Cancelling currently selected map (called when pressing Cancel-button)
    /// </summary>
    public void CancelMap()
    {
        // Hiding buttons
        acceptButton.SetActive(false);
        cancelButton.SetActive(false);

        // Clearing reference of selected map
        map = null;
    }
    #endregion
}
