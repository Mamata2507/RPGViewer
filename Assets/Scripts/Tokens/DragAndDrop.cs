using UnityEngine;
using Photon.Pun;
using System.Collections;
using FunkyCode;

namespace RPG
{
    public class DragAndDrop : MonoBehaviourPun
    {
        #region Variables
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

        private Vector3 lightPos;

        // Networking variables (hiding in inspector)
        [HideInInspector] public Photon.Realtime.Player originalOwner;
        [HideInInspector] public PhotonTransformViewClassic transformView;

        // Reference of the grid
        [HideInInspector] public GridManager grid;

        // Radius of circle collider
        private float radius;
        [HideInInspector] public float size;
        #endregion

        #region Mouse Input
        private void OnMouseDown()
        {
            // Token position is not synchronized to other clients
            transformView.m_PositionModel.SynchronizeEnabled = false;

            // Checking if info box is open
            if (infoBox.activeInHierarchy == true)
            {
                infoBox.SetActive(false);
                closeBox = true;
            }

            // Checking if info box is closed
            else if (infoBox.activeInHierarchy == false) closeBox = false;

            isPressing = true;

            // Checking if current client is master client
            if (PhotonNetwork.IsMasterClient) photonView.RequestOwnership();
            if (originalOwner != photonView.Owner) photonView.RequestOwnership();

            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            StartCoroutine(CheckDragging());
        }

        public void OnMouseUp()
        {
            // Token position is not synchronized to other clients
            transformView.m_PositionModel.SynchronizeEnabled = true;

            isPressing = false;

            StopAllCoroutines();

            // Cheking if user has not moved the token
            if (Vector3.Distance(startPos, endPos) <= 0.1f && !closeBox && !isDragging) infoBox.SetActive(!infoBox.activeInHierarchy);

            if (isDragging) GetComponent<LightManager>().myLight.transform.position = transform.position;
            if (isDragging && snapToGrid) SnapToGrid();
        }
        #endregion

        #region Start & Update
        private void Start()
        {
            transformView = GetComponent<PhotonTransformViewClassic>();

            // Setting this user to be the original owner of this token
            originalOwner = photonView.Owner;

            infoBox.SetActive(false);
        }

        private void Update()
        {
            // Getting reference of the grid
            if (FindObjectOfType<GridManager>() != null) grid = FindObjectOfType<GridManager>();

            if (grid != null && GetComponentInChildren<Light2D>().size <= 0.5)
            {
                SetScale((GetComponentInChildren<SpriteRenderer>().sprite.texture.width + GetComponentInChildren<SpriteRenderer>().sprite.texture.height) / 200f);
                GetComponent<LightManager>().myLight.size = grid.cellWidth * (60 / 5) + grid.cellHeight / 2;
            }

            // Drag token if it's mine
            if (isDragging && photonView.IsMine)
            {
                lightPos = GetComponent<LightManager>().myLight.transform.position;
                DragToken();
            }

            Canvas2D[] managers = infoBox.GetComponentsInChildren<Canvas2D>();
            bool canExit = true;
            foreach (var manager in managers)
            {
                if (manager.preventingDrag) canExit = false;
            }

            if (Input.GetMouseButtonDown(0) && infoBox.activeInHierarchy == true && canExit) infoBox.SetActive(false);
        }
        #endregion

        #region Dragging
        /// <summary>
        /// Checking if user wants to move the token or open info box
        /// </summary>
        private IEnumerator CheckDragging()
        {
            while (true)
            {
                endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector3.Distance(startPos, endPos) >= 0.1f && photonView.IsMine) isDragging = true;

                yield return new WaitForSeconds(0.01f);
            }

        }

        /// <summary>
        /// Dragging token with mouse position
        /// </summary>
        private void DragToken()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePos);
            GetComponent<LightManager>().myLight.transform.position = lightPos;
        }

        /// <summary>
        /// Snapping the token to closest cell
        /// </summary>
        public void SnapToGrid()
        {
            isDragging = false;
            transform.Translate(grid.GetClosestPosition(transform.position, size) - transform.position);
        }
        #endregion

        #region Scaling
        /// <summary>
        /// Scaling token size to match grid size
        /// </summary>
        public void SetScale(float increment)
        {
            if (grid != null)
            {
                GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(grid.cellWidth / increment * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5), grid.cellWidth / increment * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5), 1);

                if (radius != (grid.cellWidth * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5) + grid.cellWidth * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5)) / 4f)
                {
                    radius = (grid.cellWidth * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5) + grid.cellWidth * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5)) / 4f;
                    photonView.RPC("ChangeScale", RpcTarget.AllBuffered, photonView.ViewID, increment);
                }
            }
        }

        /// <summary>
        /// Set values for tokens
        /// </summary>
        public void SetValues(string name)
        {
            photonView.RPC("ChangeValues", RpcTarget.AllBuffered, photonView.ViewID, name);
        }
        #endregion

        #region RPC
        [PunRPC]
        private void ChangeValues(int viewID, string name)
        {
            if (photonView.ViewID == viewID)
            {
                gameObject.name = name;
                GetComponentInChildren<SpriteRenderer>().sprite = (Sprite)Assets.tokenTextures[name];
            }
        }

        [PunRPC]
        private void ChangeScale(int viewID, float increment)
        {
            if (photonView.ViewID == viewID)
            {
                GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(grid.cellWidth / increment * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5), grid.cellWidth / increment * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5), 1);
                GetComponent<CircleCollider2D>().radius = (grid.cellWidth * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5) + grid.cellWidth * (int.Parse(GetComponentInChildren<InfoManager>().sizeInput.text) / 5)) / 4f;
            }
        }
        #endregion
    }
}