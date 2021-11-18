using UnityEngine;
using Photon.Pun;
using System.Collections;

public class DragAndDrop : MonoBehaviourPun
{
    private bool snapToGrid = true;
    public bool isDragging = false;
    public bool isPressing = false;
    
    public GameObject infoBox;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool closeBox;

    private PhotonView photonView;
    private Photon.Realtime.Player lastOwner;
    private PhotonTransformViewClassic transformView;
    
    private Grid grid;

    private void OnMouseDown()
    {
        if (infoBox.activeInHierarchy == true)
        {
            infoBox.SetActive(false);
            closeBox = true;
        }
        else if (infoBox.activeInHierarchy == false)
        {
            closeBox = false;
        }

        isPressing = true;

        if (PhotonNetwork.IsMasterClient || lastOwner != photonView.Owner)
        {
            lastOwner = photonView.Owner;
            photonView.RequestOwnership();
        }
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StartCoroutine(CheckDragging());
    }

    public void OnMouseUp()
    {
        isPressing = false;

        StopAllCoroutines();

        if (Vector3.Distance(startPos, endPos) <= 0.1f && !closeBox)
        {
            infoBox.SetActive(!infoBox.activeInHierarchy);
        }
        
        if (isDragging) SnapToGrid();
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

    private IEnumerator CheckDragging()
    {
        while (true)
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(startPos, endPos) >= 0.1f)
            {
                isDragging = true;
            }
            yield return new WaitForSeconds(0.01f);
        }
        
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
