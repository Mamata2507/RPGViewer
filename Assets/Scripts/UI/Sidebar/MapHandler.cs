using UnityEngine;
using Photon.Pun;

public class MapHandler : MonoBehaviourPun
{
    // Accpet and Cancel buttons
    public GameObject acceptButton;
    public GameObject cancelButton;

    // Prefab of currently selected map
    public GameObject mapPrefab;

    // Mouse over GUI
    public bool canInstantiate;

    private void OnMouseOver()
    {
        canInstantiate = true;
    }

    private void OnMouseExit()
    {
        canInstantiate = false;
    }

    private void Start()
    {
        acceptButton = GetComponentInParent<MapButtons>().acceptButton;
        cancelButton = GetComponentInParent<MapButtons>().cancelButton;

        // Hiding buttons
        if (acceptButton != null) acceptButton.SetActive(false);
        if (acceptButton != null) cancelButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canInstantiate)
        {
            SelectMap();
        }

        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<CanvasManager>().preventingDrag = false;
            GetComponent<CanvasManager>().preventingZoom = false;
        }
    }

    /// <summary>
    /// Selecting clicked map
    /// </summary>
    private void SelectMap()
    {
        GetComponentInParent<MapButtons>().map = mapPrefab;

        // Showing buttons
        acceptButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    /// <summary>
    /// Instantiating selected map
    /// </summary>
    public void AcceptMap()
    {
        PhotonNetwork.Instantiate(mapPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
