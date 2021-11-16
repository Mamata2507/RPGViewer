using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DragAndDrop : MonoBehaviourPun
{
    private bool snapToGrid = true;
    public bool isDragging = false;

    private PhotonView photonView;
    private Photon.Realtime.Player lastOwner;
    private PhotonTransformViewClassic transformView;
    
    private Grid grid;

    public void OnMouseDown()
    {
        isDragging = true;
        
        if (PhotonNetwork.IsMasterClient || lastOwner != photonView.Owner)
        {
            lastOwner = photonView.Owner;
            photonView.RequestOwnership();
        }
        
    }

    public void OnMouseUp()
    {
        SnapToGrid();
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        transformView = GetComponent<PhotonTransformViewClassic>();
        lastOwner = photonView.Owner;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Map").GetComponent<Grid>() != null && photonView.IsMine) grid = GameObject.FindGameObjectWithTag("Map").GetComponent<Grid>();
        if (isDragging && photonView.IsMine) DragObject();
    }

    private void DragObject()
    {
        transformView.m_PositionModel.SynchronizeEnabled = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    public void SnapToGrid()
    {
        isDragging = false;

        if (snapToGrid && photonView.IsMine)
        {
            transform.Translate(grid.GetClosestPosition(transform.position) - transform.position);
            transformView.m_PositionModel.SynchronizeEnabled = true;
        }
    }
}
