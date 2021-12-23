using UnityEngine;
using Photon.Pun;

public class MapHandler : MonoBehaviourPun
{
    #region Variables
    // Accpet and Cancel buttons
    public GameObject acceptButton;
    public GameObject cancelButton;

    // Prefab of currently selected map
    public GameObject mapPrefab;
    public GameObject outline;

    // Mouse over GUI
    public bool canSelect;
    #endregion

    #region Mouse Input
    private void OnMouseOver()
    {
        canSelect = true;
    }

    private void OnMouseExit()
    {
        canSelect = false;
    }
    #endregion

    #region Start & Update
    private void Start()
    {
        // Getting reference of buttons
        acceptButton = GetComponentInParent<MapButtons>().acceptButton;
        cancelButton = GetComponentInParent<MapButtons>().cancelButton;

        // Hiding map selection outline
        outline.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSelect)
        {
            // Prevent camera movement
            GetComponent<Canvas2D>().preventingDrag = false;
            GetComponent<Canvas2D>().preventingZoom = false;
            
            SelectMap();
        }

        if (Input.GetMouseButtonDown(0) && !canSelect)
        {
            // Hiding selection outline if clicked outside of this map
            outline.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Enabling camera movement
            GetComponent<Canvas2D>().preventingDrag = false;
            GetComponent<Canvas2D>().preventingZoom = false;
        }
    }
    #endregion

    #region Map Handling
    /// <summary>
    /// Selecting clicked map
    /// </summary>
    private void SelectMap()
    {
        // Selecting clicked map
        GetComponentInParent<MapButtons>().map = mapPrefab;

        // Showing outline of this map to indicate it's selected
        outline.SetActive(true);

        // Showing buttons
        acceptButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    /// <summary>
    /// Instantiating selected map
    /// </summary>
    public void AcceptMap()
    {
        // Instantiating this map
        PhotonNetwork.Instantiate(mapPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
    }
    #endregion
}
