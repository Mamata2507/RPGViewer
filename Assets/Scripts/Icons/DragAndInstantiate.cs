using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DragAndInstantiate : MonoBehaviour
{
    public GameObject iconPrefab;
    private GameObject icon;

    public bool isDragging = false;
    private bool canDrag;

    private void OnMouseOver()
    {
        canDrag = true;
    }

    private void OnMouseExit()
    {
        canDrag = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canDrag)
        {
            InstantiateIcon();
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            GetComponent<CanvasManager>().preventingDrag = false;
            GetComponent<CanvasManager>().preventingZoom = false;
            icon.GetComponent<DragAndDrop>().SnapToGrid();
            isDragging = false;
        }
        if (isDragging) DragObject();
    }

    private void InstantiateIcon()
    {
        icon = PhotonNetwork.Instantiate("Prefabs/Icons/" + iconPrefab.name, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }
    
    private void DragObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (icon.GetComponent<DragAndDrop>().transformView != null) icon.GetComponent<DragAndDrop>().transformView.m_PositionModel.SynchronizeEnabled = false;
        icon.transform.position = mousePos;
    }
}
