using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DragAndDrop : MonoBehaviour
{
    private float gridSize = 0.5f;
    private bool snapToGrid = true;
    private bool isDragging = false;

    private PhotonView view;
    private PhotonTransformView transformView;
    private Grid grid;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        transformView = GetComponent<PhotonTransformView>();
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
        SnapToGrid();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Map").GetComponent<Grid>() != null && view.IsMine) grid = GameObject.FindGameObjectWithTag("Map").GetComponent<Grid>();
        if (isDragging && view.IsMine) DragObject();
    }

    private void DragObject()
    {
        if (transformView.m_SynchronizePosition == true) transformView.m_SynchronizePosition = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    private void SnapToGrid()
    {
        if (snapToGrid && view.IsMine)
        {
            Vector3 gridPos = (new Vector3(Mathf.RoundToInt(transform.position.x / grid.gridSize) * gridSize, Mathf.RoundToInt(transform.position.y / grid.gridSize) * gridSize) - transform.position);
            transform.Translate(gridPos);
            if (transformView.m_SynchronizePosition == false) transformView.m_SynchronizePosition = true;
        }
    }
}
