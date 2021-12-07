using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MapHandler : MonoBehaviourPun
{
    // Accpet and Cancel buttons
    public GameObject acceptButton;
    public GameObject cancelButton;

    // Prefab of currently selected map
    public GameObject mapPrefab;
    public GameObject outline;

    // Mouse over GUI
    public bool canSelect;

    private void OnMouseOver()
    {
        canSelect = true;
    }

    private void OnMouseExit()
    {
        canSelect = false;
    }

    private void Start()
    {
        acceptButton = GetComponentInParent<MapButtons>().acceptButton;
        cancelButton = GetComponentInParent<MapButtons>().cancelButton;

        outline.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSelect)
        {
            GetComponent<Canvas2D>().preventingDrag = false;
            GetComponent<Canvas2D>().preventingZoom = false;
            SelectMap();
        }

        if (Input.GetMouseButtonDown(0) && !canSelect)
        {
            outline.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<Canvas2D>().preventingDrag = false;
            GetComponent<Canvas2D>().preventingZoom = false;
        }
    }

    /// <summary>
    /// Selecting clicked map
    /// </summary>
    private void SelectMap()
    {
        GetComponentInParent<MapButtons>().map = mapPrefab;
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
        PhotonNetwork.Instantiate(mapPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
