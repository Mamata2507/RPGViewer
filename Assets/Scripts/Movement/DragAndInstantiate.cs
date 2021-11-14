using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DragAndInstantiate : MonoBehaviour
{
    public GameObject iconPrefab;

    private GameObject tempIcon;
    private bool isDragging = false;
    private bool canDrag = false;

    private void OnMouseOver()
    {
        canDrag = true;
    }

    private void OnMouseExit()
    {
        canDrag = false;
    }

    private void InstantiateIcon()
    {
        Destroy(tempIcon);
        GameObject icon = PhotonNetwork.Instantiate("Prefabs/Icons/" + iconPrefab.name, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        icon.transform.position = new Vector3(icon.transform.position.x, icon.transform.position.y, 0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canDrag)
        {
            Debug.Log("runs");
            tempIcon = Instantiate(iconPrefab);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            InstantiateIcon();
        }
        if (isDragging) DragObject();
    }

    private void DragObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tempIcon.transform.position = mousePos;
    }
}
