using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DragAndInstantiate : MonoBehaviour
{
    public GameObject iconPrefab;
    private GameObject icon;
    private Camera camera;

    private bool isDragging = false;
    private bool canDrag = false;

    private void OnMouseOver()
    {
        camera.GetComponent<CameraController>().canDrag = false;
        camera.GetComponent<CameraController>().canZoom = false;
        canDrag = true;
    }

    private void OnMouseExit()
    {
        camera.GetComponent<CameraController>().canZoom = false;
        if (!isDragging) camera.GetComponent<CameraController>().canDrag = true;
        canDrag = false;
    }

    private void Start()
    {
        camera = Camera.main;
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
            isDragging = false;
            icon.GetComponent<DragAndDrop>().SnapToGrid();
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
        icon.transform.position = mousePos;
    }
}
