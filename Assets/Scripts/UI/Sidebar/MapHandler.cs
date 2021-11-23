using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MapHandler : MonoBehaviourPun
{
    public GameObject acceptButton;
    public GameObject cancelButton;

    public GameObject mapPrefab;

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

    private void SelectMap()
    {
        GetComponentInParent<MapButtons>().map = mapPrefab;
        acceptButton.SetActive(true);
        cancelButton.SetActive(true);
    }

    public void AcceptMap()
    {
        PhotonNetwork.Instantiate(mapPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void CancelMap()
    {
        acceptButton.SetActive(false);
        cancelButton.SetActive(false);
    }
}
