using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DragAndInstantiate : MonoBehaviourPunCallbacks
{
    #region Variables
    // Teplate to instantiate
    [SerializeField] private GameObject tokenTemplate;

    // Currently instantiated token
    private GameObject token;

    // Dragging of the token
    public bool isDragging = false;
    private bool canDrag;

    private new PhotonView photonView;
    #endregion

    #region Mouse Input
    private void OnMouseOver()
    {
        canDrag = true;
    }

    private void OnMouseExit()
    {
        canDrag = false;
    }
    #endregion

    #region Start & Update
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        photonView.ViewID = PhotonNetwork.AllocateViewID(false);
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
            GetComponent<Canvas2D>().preventingDrag = false;
            GetComponent<Canvas2D>().preventingZoom = false;

            // Snapping token to grid
            token.GetComponent<DragAndDrop>().SnapToGrid();

            isDragging = false;
        }
        if (isDragging) DragToken();
    }
    #endregion

    #region Instantiation
    /// <summary>
    /// Instantiating this token 
    /// </summary>
    private void InstantiateToken()
    {
        // Instantiating token to mouse position
        token = PhotonNetwork.Instantiate(@"Prefabs\Tokens\" + tokenTemplate.name, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

        token.GetComponent<DragAndDrop>().SetValues(gameObject.name);

    }
    #endregion

    #region Dragging
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
    #endregion
}
