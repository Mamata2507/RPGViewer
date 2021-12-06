using UnityEngine;
using Photon.Pun;
using System.Collections;

public class DragAndDrop : MonoBehaviourPun
{
    // Dragging and pressing of token (hiding in inspector)
    [HideInInspector] public bool isDragging = false;
    [HideInInspector] public bool isPressing = false;
    
    // Snap token to grid
    [SerializeField] private bool snapToGrid;

    // Info box GameObject
    [SerializeField] private GameObject infoBox;
    
    // Mouse position when pressing and releasing LMB
    private Vector3 startPos;
    private Vector3 endPos;
    
    // Close box when pressing LMB
    private bool closeBox;

    // Networking variables (hiding in inspector)
    [HideInInspector] public Photon.Realtime.Player originalOwner;
    [HideInInspector] public PhotonTransformViewClassic transformView;

    // Photon View of this token
    private new PhotonView photonView;

    // Reference of the grid
    private GridManager grid;

    private void OnMouseDown()
    {
        // Checking if info box is open
        if (infoBox.activeInHierarchy == true)
        {
            infoBox.SetActive(false);
            
            // Preventing info box to open immediately when releasing LMB
            closeBox = true;
        }

        // Checking if info box is closed
        else if (infoBox.activeInHierarchy == false)
        {
            // Info box can now be opened when releasing LMB
            closeBox = false;
        }

        isPressing = true;

        // Checking if current client is master client
        if (PhotonNetwork.IsMasterClient)
        {
            // Transferring token ownership to master (master can now move this token)
            photonView.RequestOwnership();
        }

        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        StartCoroutine(CheckDragging());
    }

    public void OnMouseUp()
    {   
        isPressing = false;

        StopAllCoroutines();

        // Cheking if user has not moved the token
        if (Vector3.Distance(startPos, endPos) <= 0.1f && !closeBox && !isDragging) infoBox.SetActive(!infoBox.activeInHierarchy);
        
        if (isDragging) SnapToGrid();

        // Transfer ownership of this token back to the original owner
        if (photonView.Owner != originalOwner) photonView.TransferOwnership(originalOwner);
    }

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        transformView = GetComponent<PhotonTransformViewClassic>();

        // Setting this user to be the original owner of this token
        originalOwner = photonView.Owner;

        infoBox.SetActive(false);
    }

    private void Update()
    {
        // Getting reference of the grid
        if (GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>() != null && photonView.IsMine) grid = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();

        // Scaling token according to grid size
        if (grid != null) transform.localScale = new Vector3(grid.cellWidth, grid.cellWidth, 1);

        // Drag token if it's mine
        if (isDragging && photonView.IsMine) DragToken();

        if (Input.GetMouseButtonDown(0) && infoBox.activeInHierarchy == true && !infoBox.GetComponent<CanvasManager>().preventingDrag)
        {
            infoBox.SetActive(false);
        }
    }

    /// <summary>
    /// Checking if user wants to move the token or open info box
    /// </summary>
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

    /// <summary>
    /// Dragging token with mouse position
    /// </summary>
    private void DragToken()
    {
        // Token position is not synchronized to other clients
        transformView.m_PositionModel.SynchronizeEnabled = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    /// <summary>
    /// Snapping the token to closest cell
    /// </summary>
    public void SnapToGrid()
    {
        isDragging = false;

        if (snapToGrid)
        {
            // Moving token to closest cell
            transform.Translate(grid.GetClosestPosition(transform.position) - transform.position);

            // Synchronizing position to other clients
            transformView.m_PositionModel.SynchronizeEnabled = true;
        }
    }
}