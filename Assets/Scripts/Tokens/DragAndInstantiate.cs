using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DragAndInstantiate : MonoBehaviourPunCallbacks
{
    // Teplate to instantiate
    [SerializeField] private GameObject tokenTemplate;

    // Currently instantiated token
    private GameObject token;

    // Dragging of the token
    public bool isDragging = false;
    private bool canDrag;

    private PhotonView photonView;

    private void OnMouseOver()
    {
        canDrag = true;
    }

    private void OnMouseExit()
    {
        canDrag = false;
    }

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

    /// <summary>
    /// Instantiating this token 
    /// </summary>
    private void InstantiateToken()
    {
        // Instantiating token to mouse position
        token = PhotonNetwork.Instantiate(@"Prefabs\Tokens\" + tokenTemplate.name, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

        token.GetComponentInChildren<SpriteRenderer>().sprite = GetComponent<Image>().sprite;
        token.GetComponent<DragAndDrop>().myName = gameObject.name;

        photonView.RPC("ChangeName", RpcTarget.AllBuffered, token.GetComponent<DragAndDrop>().photonView.ViewID);
        photonView.RPC("ChangeImage", RpcTarget.AllBuffered, token.GetComponent<DragAndDrop>().photonView.ViewID);
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