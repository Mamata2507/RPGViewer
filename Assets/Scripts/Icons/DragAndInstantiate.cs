using UnityEngine;
using Photon.Pun;

public class DragAndInstantiate : MonoBehaviour
{
    // Teplate to instantiate
    [SerializeField] private GameObject tokenTemplate;

    // Currently instantiated token
    private GameObject token;

    // Dragging of the token
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
            InstantiateToken();

            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // User can now drag and zoom camera
            GetComponent<CanvasManager>().preventingDrag = false;
            GetComponent<CanvasManager>().preventingZoom = false;

            // Snapping token to grid
            token.GetComponent<DragAndDrop>().SnapToGrid();

            isDragging = false;
        }
        if (isDragging) DragToken();
    }

    /// <summary>
    /// Instantiating this token 
    /// </summary>
    private void InstantiateToken()
    {
        // Instantiating token to mouse position
        token = PhotonNetwork.Instantiate(@"Prefabs\Tokens\" + tokenTemplate.name, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }

    /// <summary>
    /// Dragging token with mouse position
    /// </summary>
    private void DragToken()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Token position is not synchronized to other clients
        if (token.GetComponent<DragAndDrop>().transformView != null) token.GetComponent<DragAndDrop>().transformView.m_PositionModel.SynchronizeEnabled = false;

        // Moving token to mouse position
        token.transform.position = mousePos;
    }
}
