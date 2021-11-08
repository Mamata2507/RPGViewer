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
    private PhotonTransformViewClassic transformViewClassic;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        transformViewClassic = GetComponent<PhotonTransformViewClassic>();
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
        if (isDragging && view.IsMine) DragObject();
    }

    private void DragObject()
    {
        if (transformViewClassic.m_PositionModel.SynchronizeEnabled == true) transformViewClassic.m_PositionModel.SynchronizeEnabled = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    private void SnapToGrid()
    {
        if (snapToGrid && view.IsMine)
        {
            Vector2 gridPos = (new Vector3(Mathf.RoundToInt(transform.position.x / gridSize) * gridSize, Mathf.RoundToInt(transform.position.y / gridSize) * gridSize) - transform.position);
            transform.Translate(gridPos);
            if (transformViewClassic.m_PositionModel.SynchronizeEnabled == false) transformViewClassic.m_PositionModel.SynchronizeEnabled = true;
        }
    }
}
